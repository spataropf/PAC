namespace PAC.World;

using PAC.Combat;
using PAC.Models;
using PAC.Services;
using Spectre.Console;

public class ExplorationManager
{
    private WorldManager worldManager = new WorldManager();
    private EnemyFactory enemyFactory = new EnemyFactory();
    private CombatManager combatManager = new CombatManager();
    public void Explore(Player player)
    {
        int roll = worldManager.GetRandomEvent();

        if (roll == 0)
        {
            AnsiConsole.MarkupLine("[grey]Nothing happens...[/]");
            Console.ReadKey();
        }
        else if (roll == 1)
        {
            Item? loot = combatManager.StartCombat(player, enemyFactory.CreateRandomEnemy());

            if (loot != null)
            {
                player.Inventory.AddItem(loot);
            }

            if (!player.IsAlive)
            {
                AnsiConsole.MarkupLine("[red]Game Over...[/]");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
        else
        {
            Item potion = new Item("Potion", "Heals 20 HP", 20);
            player.Inventory.AddItem(potion);

            AnsiConsole.MarkupLine("[green]You found a potion![/]");
            Console.ReadKey();
        }

    }
}