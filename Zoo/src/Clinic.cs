namespace Zoo;

interface IClinic{
    bool IsHealthy(IAlive athing);
}

class VetRandomClinic : IClinic{
    public bool IsHealthy(IAlive athing){
        Random rand = new Random(athing.GetHashCode());
        int isHealthy = rand.Next(0, 5);
        return isHealthy > 0;
    }
}