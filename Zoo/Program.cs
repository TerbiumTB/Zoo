using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using Zoo;
using Zoo.Utils;


class Programm{
    public enum AnimalKind{
        Animal,
        Monkey, 
        Rabbit,
        Tiger
    }

    public enum Action{
        Quit,
        AddAnimal,
        ShowAnimals,
        ShowContact,
        ShowFood,
        ShowThings,
        Find
    }
    public enum InventoryType{
        Any, 
        Animal,
        Thing
    }
    static void Main(string[] args){
        var services = new ServiceCollection();
        services.AddSingleton<IClinic, VetRandomClinic>();

        var serviceProvider = services.BuildServiceProvider();
        var clinic = serviceProvider.GetRequiredService<IClinic>();

        Zoopark zoo = new Zoopark(clinic);

        Monkey monkey = new Monkey(name: "Абоба", food:10);
        Rabbit rabbit = new Rabbit(name: "Роджер", food:10, kindness:8);
        Tiger tiger = new Tiger(name: "Шерхан", food:4, aggressiveness:9);
        zoo.AddAnimal(tiger);
        zoo.AddAnimal(monkey);
        zoo.AddAnimal(rabbit);
        zoo.AddThing(new Computer(120));
        zoo.AddThing(new Table(14));

        Console.WriteLine("Добро пожаловать в зоопарк!");
        while(true){
            Option.Show(msg: "Что бы вы хотели сделать:", 
                        "Выйти",
                        "Добавить новое животное в зоопарк", 
                        "Посмотреть список животных в зоопарке",
                        "Посмотреть список животных, которых можно отправить в контактный зоопарк",
                        "Посмотреть количество потребляемой животными пищи",
                        "Посмотреть список вещей, стоящих на балансе в зоопарке",
                        "Найти по инвентаризационному номеру ");

            var action = Option.Scan<Action>();    

            switch (action){
                case Action.Quit:
                    Console.WriteLine("Завершаю сессию работы зоопарка...");
                    return;

                case Action.AddAnimal:
                    IAnimal animal;

                    string? name = UtilsScan.ScanWord("Введите кличку животного (можно оставить безымяным): ");
                    int kindness, aggressiveness, food;

                    Option.Show(msg: "Животного какого вида вы хотите добавить:",
                                "Неизвестный", 
                                "Обезьяна",
                                "Кролик", 
                                "Тигр");

                    var levelCond = Conditional.IntervalCondition(0, 10);
                    string kindErr = "Введите уровень доброты от 0 до 10: ";
                    string aggErr = "Введите уровень агрессии от 0 до 10: ";

                    var foodCond = Conditional.PositiveCondition();
                    string foodErr = "Количество кг должно быть положительным. Введите количество киллограммов:";

                    var kind = Option.Scan<AnimalKind>();
                    switch(kind){
                        case AnimalKind.Monkey:
                            kindness = UtilsScan.ScanInt("Насколько обезьяна добрая? ", kindErr, levelCond);
                            food = UtilsScan.ScanInt("Сколько кг бананов обезбяна ест за день? ", foodErr, foodCond);
                            animal = new Monkey(name: name, kindness: kindness, food: food);
                            break;
                        case AnimalKind.Rabbit:
                            kindness = UtilsScan.ScanInt("Насколько кролик добрый? ", kindErr, levelCond);
                            food = UtilsScan.ScanInt("Сколько кг морковки кролик ест за день? ", foodErr, foodCond);
                            animal = new Rabbit(name: name, kindness: kindness, food: food);
                            break;
                        case AnimalKind.Tiger:
                            aggressiveness = UtilsScan.ScanInt("Насколько тигр агрессивный? ", aggErr, levelCond);
                            food = UtilsScan.ScanInt("Сколько кг мяса тигр ест за день? ", foodErr, foodCond);
                            animal = new Tiger(name: name, aggressiveness: aggressiveness, food: food);
                            break;
                        // case Animal
                        default: 
                            food = UtilsScan.ScanInt("Сколько кг пищи животное ест за день? ", "", foodCond);
                            animal = new Animal(name: name, food: food);
                            break;
                    }
                    zoo.AddAnimal(animal);
                    break;

                case Action.ShowAnimals:
                    if (zoo.EmptyAnimal()){
                        Console.WriteLine("В зоопарке сейчас нет животных");
                        break;
                    }
                    Console.WriteLine("Животные, содержащиеся в зоопарке:");
                    foreach(IAnimal zoo_animal in zoo.Animals){
                        Console.WriteLine(zoo_animal);
                    }
                    break;

                case Action.ShowContact:
                    var contactAnimals = zoo.FilterContactZooAnimals();
                    if(contactAnimals.Count == 0){
                        Console.WriteLine("В зоопарке сейчас нет животных, которых можно отправить в контактный зоопарк");
                        break;
                    }
                    Console.WriteLine("Животные, которых можно отправить в контактный зоопарк:");
                    foreach(IAnimal contactAnimal in contactAnimals){
                        Console.WriteLine(contactAnimal);
                    }
                    break;

                case Action.ShowFood:
                    if (zoo.EmptyAnimal()){
                        Console.WriteLine("В зоопарке сейчас нет животных");
                        break;
                    }
                    Console.WriteLine("Количество (в кг) потребляемой еды всеми животными зоопарка в день: {0}", zoo.SumFoodAmount());
                    break;

                case Action.ShowThings:
                    if (zoo.EmptyThings()){
                        Console.WriteLine("В зоопарке сейчас нет инвентаризованных вещей");
                        break;
                    }
                    Console.WriteLine("Инвентаризованные вещи зоопарка:");
                    foreach(IThing thing in zoo.Things){
                        Console.WriteLine(thing);
                    }
                    break;   
                case Action.Find:
                    IInventory? inventory;
                    int number = UtilsScan.ScanInt("Введите инвентаризационный номер: ", "Номер должен быть положительным", Conditional.PositiveCondition());
                    Option.Show(msg: "Какой тип инвентаря вы хотели бы найти:", 
                        "Любой",
                        "Животное", 
                        "Вещь");
                    var inventoryType = Option.Scan<InventoryType>();
                    switch (inventoryType){
                        case InventoryType.Any: 
                            inventory = zoo.FindInventory(number);
                            if (inventory is null){
                                Console.WriteLine("Инвентаря с таким номером не существует");
                            }
                            Console.WriteLine(inventory);
                            break;
                        case InventoryType.Animal: 
                            inventory = zoo.FindAnimal(number);
                            if (inventory is null){
                                Console.WriteLine("Животного с таким номером не существует");
                            }
                            Console.WriteLine(inventory);
                            break;
                        case InventoryType.Thing: 
                            inventory = zoo.FindThing(number);
                            if (inventory is null){
                                Console.WriteLine("Вещи с таким номером не существует");
                            }
                            Console.WriteLine(inventory);
                            break;
                    }    
                    break;
            }
        }
    }
}
