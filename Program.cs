using System.Text.Json;
using System.Text;

// var jsonObject = File.ReadAllText("map.json");
// var NumMap = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string,string>>>(jsonObject);
// Input.TestFunction();

StringBuilder builder = new();
builder.Append("A");
builder.Append("B");
Console.WriteLine(builder.ToString());


// Money test = new() {Value = "23"};
// Console.WriteLine(test.Cents);

class Input
{
    public static void TestFunction(){
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
    
    public static string Convert2n(string value, Dictionary<string, Dictionary<string,string>> map) {
        if (value.Length > 2){
            throw new ArgumentException("Input is not a 2-digit number");
        }
        if (value.Length == 1){
            return map["tens"][value];
        }
        string a = map["ones"][value];
        return a;
    }
}
class DigitMap
{ 
    public required Dictionary<string, string> MapOnes {get; set;}
    public required Dictionary<string, string> MapTens {get; set;}
    public required Dictionary<string, string> MapZeros {get; set;}
}

