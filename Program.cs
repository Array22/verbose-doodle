using System.Text.Json;

var wallet = new Money("00120.00");
Console.WriteLine(wallet.ConvertMoney());

class Money {

    public string Cash {get; set;}

    static Money()
    {
        var map = Setup.StartMaps();
        StartMaps(map);
    }

    public Money(string num)
    {
        if (float.TryParse(num, out float result) == false)
        {
            throw new ArgumentException("Please input a valid number.");
        }
        Cash = num;
    }

    private static Dictionary<string,string> OnesMap {get;set;} = null!;
    private static Dictionary<string,string> TensMap {get;set;} = null!;
    private static Dictionary<string,string> ZerosMap {get;set;} = null!;

    public static void StartMaps(Dictionary<string, Dictionary<string,string>> map)
    {
        OnesMap = map["ones"] 
            ?? throw new MissingMemberException("OnesMap not available. Check map.json file");
        TensMap = map["tens"]
            ?? throw new MissingMemberException("TensMap not available. Check map.json file");
        ZerosMap = map["zeros"]
            ?? throw new MissingMemberException("ZerosMap not available. Check map.json file");
    }

    private static string Convert2n(string num) {
        if (num.Length != 2){
            throw new ArgumentException("Input is not a 2-digit number");
        }
        string part0 = num[0].ToString();
        string part1 = num[1].ToString();
        List<string> values = [];
        values.Add(TensMap[part0]);
        if (part1 != "0")
        {
            values.Add(OnesMap[part1]);
        }
        string words = "";
        if (part0 == "0")
        {
            words = OnesMap[part1];
        }
        else
        {
            words = string.Join("-",values);
        }
        if (part0 == "1")
        {
            words = OnesMap[num];
        }
        return words;
    }

    private static string Convert3n(string num)
    {
        if (num.Length != 3)
        {
            throw new ArgumentException("Input is not a 3-digit number");
        }
        string part1 = OnesMap[num[0].ToString()];
        string words = $"{part1} HUNDRED";
        if (num[1..3] == "00")
        {
            return words;
        }
        string part2 = Convert2n(num[1..3]);
        List<string> values = [];
        if (part1 != "ZERO")
        {
            values.Add($"{words} AND");
        }
        values.Add(part2);
        string words_f = string.Join(" ", values);
        return words_f;
    }

    private static string ConvertCents(string num)
    {
        if (num.Length > 2){
            throw new ArgumentException("Please round number to 2 decimal places");
        }
        string words = "";
        if (num.Length == 1)
        {
            words = TensMap[num];
        }
        else
        {
            words = Convert2n(num);
        }
        string cent = "CENTS";
        if (words == "ONE")
        {
            cent = "CENT";
        }
        return $"{words} {cent}";
    }

    private static string ConvertDollars(string num)
    {
        bool ZerosMapCheck = false;
        int i = 0;
        int i_rev = num.Length - 1;
        List<string> values = [];
        while (i < num.Length)
        {
            if (i_rev % 3 == 2)
            {
                if (num[i..(i+3)] != "000")
                {
                    values.Add(Convert3n(num[i..(i+3)]));
                    ZerosMapCheck = true;
                }
                i += 3;
            }
            else if (i_rev % 3 == 1)
            {
                if (num[i..(i+2)] != "00")
                {
                    values.Add(Convert2n(num[i..(i+2)]));
                    ZerosMapCheck = true;
                }
                i += 2;
            }
            else
            {
                values.Add(OnesMap[num[i].ToString()]);
                ZerosMapCheck = true;
                i += 1;
            }
            i_rev = num.Length - 1 - i;
            if (i_rev > 0 && (i_rev+1) % 3 == 0 && ZerosMapCheck)
            {
                values.Add(ZerosMap[(i_rev + 1).ToString()]);
                ZerosMapCheck = false;
            }
        }
        string words = string.Join(" ", values);
        if (words.Trim() == "ONE")
        {
            return "ONE DOLLAR";
        }
        if (words == "")
        {
            words = "ZERO";
        }
        return $"{words} DOLLARS";
    }

    public string ConvertMoney()
    {
        string[] words = Cash.Split('.');
        string dollars = ConvertDollars(words[0]);
        if (words.Length > 1)
        {
            string cents = ConvertCents(words[1]);
            List<string> values = [];
            if (dollars != "")
            {
                values.Add($"{dollars} AND");
            }
            values.Add(cents);
            string response = string.Join(" ", values);
            return response;
        }
        return dollars;
    }
}

class Setup
{
    public static Dictionary<string, Dictionary<string,string>> StartMaps()
    {
        if (!File.Exists("map.json")){
            throw new FileNotFoundException("map.json is not found in bin folder");
        }
        var jsonObject = File.ReadAllText("map.json");
        var NumMap = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string,string>>>(jsonObject)
            ?? throw new InvalidDataException("map.json missing or written in invalid format");
        return NumMap;
    }
}
