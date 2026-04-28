namespace PAC.World;

public class WorldManager
{
    private Random random = new Random();

    public int GetRandomEvent()
    {
        return random.Next(0, 3);
    }
}