
interface IPerson
{
    string Height {get;}
}

interface IHuman
{
    int Legs {get;}
}

class Person : IPerson, IHuman
{ 
    public int Legs {get; set;} = 2;
    public string Height {get; set;} = "170";
}

// class Money {
//     public string ConvertCents() {

//     }
// }

class Input
{
    public static void TestFunction(){
        Console.Write("Please input a value: ");
        string value = Console.ReadLine() ?? "";
        Console.WriteLine(value);
    }
    
}

class Program {
    static async Task Main(string[] args)
    {
        string value = "123.";
        string[] parts = value.Split('.');
        string a = parts[0]; 
        string b = parts[1];
        Console.WriteLine($"{a} AND {b}");


    }
}