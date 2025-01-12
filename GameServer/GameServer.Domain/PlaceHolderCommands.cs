using GameServer.Abstractions;

namespace GameServer;

public class PlaceHolderCommands : IPlaceHolderCommands
{
    public string HelloWorld()
    {
        return "Hello World!~";
    }
}