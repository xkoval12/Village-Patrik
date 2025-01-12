namespace GameServer;

public class Stonesmith : IHouse
{
    public int HouseNumber { get; }
    public int Storage { get; private set; }

    public Stonesmith(int houseNumber, int storage)
    {
        Storage = storage;
        HouseNumber = houseNumber; 
    }

    public void Run()
    {
        Storage++;
    }
}