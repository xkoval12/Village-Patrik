namespace GameServer;

public class Quarry : IHouse
{
    public int HouseNumber { get; }
    public int Storage { get; private set; }

    public Quarry(int houseNumber, int storage)
    {
        Storage = storage;
        HouseNumber = houseNumber; 
    }

    public void Run()
    {
        Storage++;
    }
}