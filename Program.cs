using System.Text.Json;


class DigitMap
{ 
    public int Legs {get; set;} = 2;
    public string Height {get; set;} = "170";
}

class Money {

    public string Dollar {get; set;} = "";
    public string Cents {get; set;} = "";
    public bool IsWhole {get; set;} = false;
    public string ConvertCents() {
        throw new NotImplementedException();
    }
}

class Input
{
    public static void TestFunction(){
        Console.Write("Please input a value: ");
        string Value = Console.ReadLine() ?? "";
        Console.WriteLine(Value);
        string[] Word = Value.Split('.');
        string a = Word[0]; string b = Word[1];
        Console.WriteLine($"{a} AND {b}");

    }
}

class Program {
    public static void Main(string[] args)
    {
        var jsonObject = File.ReadAllText("map.json");
        var map = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonObject);
        // Input.TestFunction();
        if (map != null)
        {
            Console.WriteLine(map["ones"]);
        }

    }
}