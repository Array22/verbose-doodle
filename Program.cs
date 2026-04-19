using System.Text.Json;
using System.Text;


// Input.TestFunction();

// StringBuilder builder = new();
// builder.Append("A");
// builder.Append("B");
// Console.WriteLine(builder.ToString());

Setup.Start();
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

    
    public static string Convert2n(string num) {
        if (num.Length > 2){
            throw new ArgumentException("Input is not a 2-digit number");
        }
        if (num.Length == 1){
            return TensMap[num];
        }
        List<string> values = [];
        

        string a = OnesMap[num];
        return a;
    }

    public static string Convert3n(string num)
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

    public static string ConvertCents(string num)
    {
        if (num.Length > 2){
            throw new ArgumentException("Please round number to 2 decimal places");
        }
        string words = Convert2n(num);
        return $"{words} CENTS";
    }

    public static string ConvertDollars(string num)
    {
        
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