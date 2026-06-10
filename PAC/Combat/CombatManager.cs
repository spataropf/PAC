using PAC.Interfaces;
using PAC.Models;
using Spectre.Console;

namespace PAC.Combat;

public class CombatManager
{
    private Random random = new Random();

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


            IAttack attack;

            if (random.Next(0, 2) == 0)
            {
                attack = new CriticalAttack();
                AnsiConsole.MarkupLine("[yellow]Critical hit![/]");
            }
            else
            {
                attack = new NormalAttack();
            }


            int damage = attack.CalculateDamage(player.Attack);
            AnsiConsole.MarkupLine($"[green]You deal {damage} damage![/]");
            Console.ReadKey();

            enemy.TakeDamage(damage);

            if (!enemy.IsAlive)
                break;

            player.TakeDamage(enemy.Attack);
        }

        AnsiConsole.Clear();

        if (player.IsAlive)
        {
            AnsiConsole.MarkupLine("[green]Victory![/]");
            player.GainExperience(enemy.RewardXp);

            Item potion = new Item("Potion", "Heals 20 HP", 20);
            AnsiConsole.MarkupLine("[green]You found a potion![/]");
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