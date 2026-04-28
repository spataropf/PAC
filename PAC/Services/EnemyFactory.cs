using PAC.Models;

namespace PAC.Services;

public class EnemyFactory
{
    private Random random = new Random();

    public Enemy CreateRandomEnemy()
    {
        int roll = random.Next(0, 3);

        if (roll == 0)
            return new Enemy("Goblin", 50, 5, 50);
        else if (roll == 1)
            return new Enemy("Orc", 80, 8, 80);
        else
            return new Enemy("Skeleton", 40, 6, 60);
    }
}