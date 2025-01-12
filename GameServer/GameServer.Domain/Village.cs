using Domain.Contract;

namespace GameServer;

public class Village
{
    private List<IHouse> houses;
    public Lumberjack Lumberjack { get; }

    public Village()
    {
        houses = new List<IHouse>();
        houses.Add( new Lumberjack(65, 0));
        houses.Add( new Stonesmith(65, 0));
        houses.Add( new Quarry(65, 0));
    }

    public VillageDto ToDto()
    {
        return new VillageDto( new LumberjackDto(Lumberjack.HouseNumber, Lumberjack.Storage));
    }
}