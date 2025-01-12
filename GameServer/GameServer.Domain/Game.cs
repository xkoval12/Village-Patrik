namespace GameServer;

public class Game
{
    private Village _village;
    public Game()
    {
        _village = new Village();
    }
    public Village Village() => _village;
    public void Run()
    {
        // Example of game logic
        Console.WriteLine("Game is starting...");
        Thread.Sleep(50000); // TODO: Simulating the game running, remove afterwards
        Console.WriteLine("Game has finished.");
    }
}