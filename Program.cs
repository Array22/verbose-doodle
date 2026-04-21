using System.Text.Json;

// Input.TestFunction();
// Setup.Start();

// Money test = new() {Value = "23"};
// Console.WriteLine(test.Cents);

class Input
{
    public static void Start(){
        Console.Write("Please input a value: ");
        string value = Console.ReadLine() ?? "";
        if (float.TryParse(value, out float result) == false)
        {
            throw new ArgumentException("Please input a valid number.");
        }
        string[] word = value.Split('.');
        string a = word[0]; string b = word[1];
        Console.WriteLine($"{a} AND {b}");

    }
}
class Money {

    public float Cash {get; set;} = 0;
    private static Dictionary<string,string> OnesMap {get;set;} = null!;
    //needs to be static because all use same map       public required Dictionary<string,string> TensMap {get;set;}
    private static Dictionary<string,string> TensMap {get;set;} = null!;
    private static Dictionary<string,string> ZerosMap {get;set;} = null!;

    
    private static string Convert2n(string num) {
        if (num.Length != 2){
            throw new ArgumentException("Input is not a 2-digit number");
        }
        string part0 = num[0].ToString();
        string part1 = num[1].ToString();
        List<string> values = [];
        values.Add(TensMap[part0]);
        values.Add(OnesMap[part1]);
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
        List<string> values = [];
        values.Add($"{OnesMap[num]} HUNDRED AND");
        values.Add(Convert2n(num[1..3]));
        string words = string.Join(" ", values);
        return words;
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
        return $"{words} CENTS";
    }

    private static string ConvertDollars(string num)
    {
        int i = 0;
        int i_rev = num.Length - 1;
        List<string> values = [];
        while (i < num.Length)
        {
            if (i_rev % 3 == 2 && num[i..(i+3)] != "000")
            {
                values.Add(Convert3n(num[i..(i+3)]));
                i += 3;
            }
            else if (i_rev % 3 == 1 && num[i..(i+2)] != "00")
            {
                values.Add(Convert2n(num[i..(i+2)]));
                i += 2;
            }
            else
            {
                values.Add(OnesMap[num[i].ToString()]);
                i += 1;
            }
            i_rev = num.Length - 1 - i;
            if (i > 0 && (i_rev+1) % 3 == 0)
            {
                values.Add(ZerosMap[(i_rev + 1).ToString()]);
            }
        }
        string words = string.Join(" ", values);
        if (words == "ONE")
        {
            return "ONE DOLLAR";
        }
        return words;
    }

    public static string ConvertMoney(string value)
    {
        string[] words = value.Split('.');
        string dollars = ConvertDollars(words[0]);
        if (words[1] is not null)
        {
            string cents = ConvertCents(words[1]);
            return $"{dollars} AND {cents}";
        }
        return dollars;
    }
}

class Setup
{
    public static void Start()
    {
        if (!File.Exists("map.json")){
            throw new FileNotFoundException("map.json is not found in bin folder");
        }
        var jsonObject = File.ReadAllText("map.json");
        var NumMap = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string,string>>>(jsonObject)
            ?? throw new InvalidDataException("");
        Dictionary<string,string> OnesMap = NumMap["ones"] 
            ?? throw new MissingMemberException("OnesMap not available. Check map.json file");
        Dictionary<string,string> TensMap = NumMap["tens"] 
            ?? throw new MissingMemberException("TensMap not available. Check map.json file");
        Dictionary<string,string> ZerosMap = NumMap["zeros"] 
            ?? throw new MissingMemberException("ZerosMap not available. Check map.json file");
        Console.WriteLine(OnesMap["1"]);
    }
}
