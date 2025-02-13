using System.Runtime.CompilerServices;

namespace Zoo.Utils;

static class Option{
    public static void Show(string msg, params string[] options){
        Console.WriteLine(msg);
        for (int i = 0; i < options.Length; ++i){
            Console.WriteLine("\t{0}. {1}", i, options[i]);
        }
    }
    public static T Scan<T>() where T: struct, Enum{
        T option;
        Console.Write("Введите номер опции: ");
        string? input = Console.ReadLine();      
        while(!Enum.TryParse(input, out option) || !Enum.IsDefined(option)){
            Console.Write("Недоступная опция. Введите коррекный номер опции: ");
            input = Console.ReadLine();
        } 
        return option;
    }
}

// public delegate bool Condition(params int[] x);
public delegate bool Condition(int x);

static class Conditional{
    public static Condition IntervalCondition(int a, int b) 
    => delegate (int x){ return a <= x && x <= b; };

    public static Condition PositiveCondition()
    =>  delegate (int x){ return x > 0; };
    public static Condition NonnegativeCondition()
    =>  delegate (int x){ return x >= 0; };

}


static class UtilsScan{
    public static int ScanInt(string msg, string errorMsg, Condition condition){
        Console.Write(msg);
        string? input = Console.ReadLine();
        int output;
        // int.TryParse(input, out int output);
        while(!int.TryParse(input, out output) || !condition(output)){
            Console.Write(errorMsg);
            input = Console.ReadLine();
        } 
        return output;
    }
    public static string? ScanWord(string msg){
        Console.Write(msg);
        string? output = Console.ReadLine();
        if(String.IsNullOrEmpty(output)){
            return null;
        }
        while(!output.All(Char.IsLetter)){
            if (String.IsNullOrEmpty(output)){
                return null;
            }
            Console.Write("Некорректный ввод. Введите слово без пробелов или пустую строку: ");
        }

        return output;
    }
}
