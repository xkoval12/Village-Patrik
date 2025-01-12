namespace GameServer;

public class Lumberjack : IHouse
{
    public int HouseNumber { get; }
    public int Storage { get; private set; }
    
    // TimeSpan _timeRate;
    // private int _itemRate;

    public Lumberjack(int houseNumber,  int storage)
    {
        HouseNumber = houseNumber;
        Storage = storage;
    }

    public void Run()
    {
        Storage++;
    }
}