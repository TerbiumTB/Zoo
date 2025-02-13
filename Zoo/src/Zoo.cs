namespace Zoo;

class Zoopark{
    private readonly IClinic clinic;
    private int _currentNumber = 1;

    private readonly List<IThing> _things = [];
    public IReadOnlyCollection<IThing> Things{
        get => _things;
    }

    private readonly List<IAnimal> _animals = [];
    public IReadOnlyCollection<IAnimal> Animals{
        get => _animals;
    }

    public Zoopark(IClinic clinic) {
        this.clinic = clinic;
    }

    public bool EmptyAnimal(){
        return _animals.Count == 0;
    }

    public bool EmptyThings(){
        return _things.Count == 0;
    }

    public void AddAnimal(IAnimal animal){
        if (!clinic.IsHealthy(animal)){
            Console.WriteLine("Животное нездорово, мы не можем принять его в зоопарк");
            return;
        }
        animal.Number = _currentNumber++;
        _animals.Add(animal);
    }

    public void AddThing(IThing thing){
        thing.Number = _currentNumber++;
        _things.Add(thing);
    }
    
    public IInventory? FindInventory(int number){
        foreach(IInventory inventory in _things){
            if (inventory.Number == number){
                return inventory;
            }
        }

        foreach(IInventory inventory in _animals){
            if (inventory.Number == number){
                return inventory;
            }
        }

        return null;
    }

    public IAnimal? FindAnimal(int number){
        foreach(IAnimal animal in _animals){
            if (animal.Number == number){
                return animal;
            }
        }
        return null;
    }

    public IThing? FindThing(int number){
        foreach(IThing thing in _things){
            if (thing.Number == number){
                return thing;
            }
        }
        return null;
    }

    public int SumFoodAmount() {
        int amount = 0;
        foreach(IAnimal animal in _animals){
            amount += animal.Food;
        }
        return amount;
    }

    public IReadOnlyCollection<IHerbo> FilterContactZooAnimals(){
        List<IHerbo> contactAnimals = [];
        foreach(IAnimal animal in _animals){
            if (animal is IHerbo herbo){
                if (herbo.Kindness >= 5){
                    contactAnimals.Add(herbo);
                }
            } 
        }
        return contactAnimals;
    }
}