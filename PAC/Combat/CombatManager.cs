using PAC.Interfaces;
using PAC.Models;
using Spectre.Console;
namespace PAC.Combat;

public class CombatManager
{
    public Item? StartCombat(Player player, Enemy enemy)
    {
        while (player.IsAlive && enemy.IsAlive)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new Panel(
                    $"[green]{player.Name}[/] : {player.Health}/{player.MaxHealth} HP\n" +
                    $"[red]{enemy.Name}[/] : {enemy.Health}/{enemy.MaxHealth} HP")
                .Header("[yellow]Combat[/]")
                .Border(BoxBorder.Rounded)
            );

            AnsiConsole.MarkupLine("[grey]Press a key to attack...[/]");
            Console.ReadKey();

            enemy.TakeDamage(player.Attack);

            if (!enemy.IsAlive)
                break;

            player.TakeDamage(enemy.Attack);
        }

        Console.Clear();

        if (player.IsAlive)
        {
            AnsiConsole.MarkupLine("[green]Victory![/]");
            player.GainExperience(enemy.RewardXp);

            Item potion = new Item("Potion", "Heals 20 HP", 20);
            Console.WriteLine("You found a potion!");
            Console.ReadKey();

            return potion;
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Defeat...[/]");
            Console.ReadKey();
            return null;
        }
    }
}