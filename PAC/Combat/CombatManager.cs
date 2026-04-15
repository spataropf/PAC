using PAC.Models;

namespace PAC.Combat;

public class CombatManager
{
    public void StartCombat(Player player, Enemy enemy)
    {
        while (player.IsAlive && enemy.IsAlive)
        {
            Console.Clear();

            Console.WriteLine("=== Fight ===");
            Console.WriteLine($"{player.Name} : {player.Health}/{player.MaxHealth} HP");
            Console.WriteLine($"{enemy.Name} : {enemy.Health}/{enemy.MaxHealth} HP");
            Console.WriteLine();
            Console.WriteLine("Press any key to attack.");

            Console.ReadKey();

            enemy.TakeDamage(player.Attack);

            if (!enemy.IsAlive)
                break;

            player.TakeDamage(enemy.Attack);
        }

        Console.Clear();

        if (player.IsAlive)
        {
            Console.WriteLine("Victory!");
            player.GainExperience(enemy.RewardXp);
        }
        else
        {
            Console.WriteLine("Defeat...");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}