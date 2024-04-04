using System;

public interface ICustomString : IComparable, ICloneable
{
    int ConvertToInt();
    void Clear();
    string Characters { get; }
}

public class String : ICustomString, IComparable, ICloneable
{
    protected string characters;

    public String()
    {
        characters = "";
    }

    public String(string str)
    {
        characters = str;
    }

    public int ConvertToInt()
    {
        int result;
        if (int.TryParse(characters, out result))
        {
            return result;
        }
        else
        {
            throw new InvalidOperationException("Unable to convert characters to int.");
        }
    }

    public void Clear()
    {
        characters = "";
    }

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;
        String other = (String)obj;
        return this.ConvertToInt().CompareTo(other.ConvertToInt());
    }

    public object Clone()
    {
        return new String(this.characters);
    }

    public string Characters { get => characters; }
}

public class DecimalString : String
{
    public DecimalString(decimal number) : base(number.ToString()) { }

    public new string Characters { get => base.Characters; }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Введіть кількість стрічок:");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
        {
            Console.WriteLine("Будь ласка, введіть додатне ціле число.");
        }

        ICustomString[] customStrings = new ICustomString[count];
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Введіть стрічку {i + 1} (десяткове число):");
            string input = Console.ReadLine();
            decimal number;
            while (!decimal.TryParse(input, out number))
            {
                Console.WriteLine("Неправильний ввід. Будь ласка, введіть десяткове число.");
                input = Console.ReadLine();
            }
            customStrings[i] = new DecimalString(number);
        }

        Array.Sort(customStrings);

        Console.WriteLine("Відсортований масив стрічок:");
        foreach (var item in customStrings)
        {
            if (item is String str)
            {
                Console.WriteLine(str.Characters);
            }
        }
    }
}
