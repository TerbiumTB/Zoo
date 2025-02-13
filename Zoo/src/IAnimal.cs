namespace Zoo;

interface IAnimal: IAlive, IInventory{
    string? Name {get; set;}
}

interface IHerbo: IAnimal {
    int Kindness {get; set;}
    
}

interface IPredator : IAnimal {
    int Aggressiveness {get; set;}
}