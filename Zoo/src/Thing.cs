namespace Zoo;

class Table: IThing{
    public int Price {get; set;}
    public int Number {get; set;} 

    public Table(int price){
        Price = price;
    }
    public override string ToString(){
        string decription = string.Format("Стол под номером {0}: \n\t Цена {1}", Number, Price);
        return decription;
    }
}

class Computer: IThing{
    public int Price {get; set;}
    public int Number {get; set;} 

    public Computer(int price){
        Price = price;
    }

    public override string ToString(){
        string decription = string.Format("Компьютер под номером {0}: \n\t Цена {1}", Number, Price);
        return decription;
    }
}