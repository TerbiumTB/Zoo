using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
// using Random;

namespace Zoo;


class Animal: IAnimal{
    public Animal(int food, string? name=null){
        Food = food;
        Name = name;
    }
    public string? Name {get; set;}

    public int Food {get; set;}

    public int Number {get; set;}

    public override string ToString(){
        string description = 
        string.Format("Животное номер {0}: \n\t Вид: Неизвестно \n\t Кличка: {1} \n\t Количество пищи в день: {2} кг",
        Number, Name ?? "Отсутвует", Food);
    
        return description;
    }
}
class Monkey: IHerbo{
    public Monkey(int food, string? name = null){
        Random rand = new Random();
        Kindness = rand.Next(0, 10);
        Food = food;
        Name = name;
    
    }
    public Monkey(int food, int kindness, string? name = null){
        Food = food;
        Kindness = kindness;
        Name = name;
    }
    public string? Name {get; set;}

    public int Food {get; set;}
    public int Kindness {get; set;}

    public int Number {get; set;}

    public override string ToString(){
        string description = 
        string.Format("Животное номер {0}: \n\t Вид: Обезьяна \n\t Кличка: {1} \n\t Количество бананов в день: {2} кг \n\t Уровень доброты: {3}",
        Number, Name ?? "Отсутвует", Food, Kindness);
    
        return description;
    }
}

class Rabbit: IHerbo{
     public Rabbit(int food, string? name = null){
        Random rand = new Random();
        Kindness = rand.Next(0, 10);
        Food = food;
        Name = name;
    
    }
    public Rabbit(int food, int kindness, string? name = null){
        Food = food;
        Kindness = kindness;
        Name = name;
    }
    public string? Name {get; set;}

    public int Food {get; set;}
    public int Kindness {get; set;}
    public int Number {get; set;}

    public override string ToString(){
        string description = 
        string.Format("Животное номер {0}: \n\t Вид: Кролик \n\t Кличка: {1} \n\t Количество морковки в день: {2} кг \n\t Уровень доброты: {3}",
        Number, Name ?? "Отсутвует", Food, Kindness);
    
        return description;
    }
}


class Tiger: IPredator{
    public Tiger(int food, string? name = null){
        Random rand = new Random();
        Aggressiveness = rand.Next(0, 10);
        Food = food;
        Name = name;
    
    }
    public Tiger(int food, int aggressiveness, string? name = null){
        Food = food;
        Aggressiveness = aggressiveness;
        Name = name;
    }

    public string? Name {get; set;}

    public int Food {get; set;}

    public int Aggressiveness {get;  set;}

    public int Number {get; set;}

    public override string ToString(){
        string description = 
        string.Format("Животное номер {0}: \n\t Вид: Тигр \n\t Кличка: {1} \n\t Количество мяса в день: {2} кг \n\t Уровень аггрессивности: {3}",
        Number, Name ?? "Отсутвует", Food, Aggressiveness);
    
        return description;
    }
}