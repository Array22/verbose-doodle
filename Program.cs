using System.Text.Json;

// var jsonObject = File.ReadAllText("map.json");
// var map = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string,string>>>(jsonObject);
Input.TestFunction();


// Money test = new() {Value = "23"};
// Console.WriteLine(test.Cents);


class DigitMap
{ 
    public required Dictionary<string, string> MapOnes {get; set;}
    public required Dictionary<string, string> MapTens {get; set;}
    public required Dictionary<string, string> MapZeros {get; set;}
}

class Money {

    public float Value {get; set;} = 0;
    public string Cents {get; set;} = "";
    public string ConvertCents() {
        throw new NotImplementedException();
    }
}

class Input
{
    public static void TestFunction(){
        Console.Write("Please input a value: ");
        string Value = Console.ReadLine() ?? "";
        if (float.TryParse(Value, out float result) == false)
        {
            throw new ArgumentException("Please input a valid number.");
        }
        Console.WriteLine(result);
        string[] Word = Value.Split('.');
        string a = Word[0]; string b = Word[1];
        Console.WriteLine($"{a} AND {b}");

    }
}