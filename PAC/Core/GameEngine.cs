using PAC.Models;
using PAC.Combat;
using Spectre.Console;

namespace PAC.Core;

public class GameEngine
{
    private Player player;
    private GameState state;

    public void Start()
    {
        state = GameState.Menu;
        Run();
    }

    private void Run()
    {
        while (true)
        {
            switch (state)
            {
                case GameState.Menu:
                    ShowMenu();
                    break;

                case GameState.Exploration:
                    Explore();
                    break;

                case GameState.Combat:
                    StartCombat();
                    break;
            }
        }
    }

    private void ShowMenu()
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(
            new FigletText("Mini RPG")
                .Centered()
                .Color(Color.Green));

        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Choose an option :[/]")
                .AddChoices("New Game", "Quit"));

        if (choice == "New Game")
        {
            string playerName = AnsiConsole.Ask<string>("[green]Player Name :[/]");

            player = new Player(playerName);
            state = GameState.Exploration;
        }
        else if (choice == "Quit")
        {
            Environment.Exit(0);
        }
    }

    private void Explore()
    {
        while (state == GameState.Exploration)
        {
            Console.Clear();

            Console.WriteLine($"=== Exploration ===");
            Console.WriteLine($"Player : {player.Name}");
            Console.WriteLine($"HP : {player.Health}/{player.MaxHealth}");
            Console.WriteLine();
            Console.WriteLine("1. Exploring");
            Console.WriteLine("2. View Inventory");
            Console.WriteLine("3. Return to Menu");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Enemy enemy = new Enemy("Goblin", 50, 5, 50);

                    CombatManager combat = new CombatManager();
                    combat.StartCombat(player, enemy);

                    if (!player.IsAlive)
                    {
                        Console.WriteLine("Game Over...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    break;

                case "2":
                    ShowInventory();
                    break;

                case "3":
                    state = GameState.Menu;
                    break;
            }
        }
    }
    private void ShowInventory()
    {
        Console.Clear();
        Console.WriteLine("=== Inventory ===");

        List<Item> items = player.Inventory.GetItems();

        if (items.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
        }
        else
        {
            foreach (Item item in items)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }

        Console.WriteLine("Press any key to return.");
        Console.ReadKey();
    }

    private void StartCombat()
    {
    }
}