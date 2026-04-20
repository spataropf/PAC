using PAC.Models;
using PAC.Combat;
using Spectre.Console;
using PAC.UI;
namespace PAC.Core;

public class GameEngine
{
    private Player player;
    private GameState state;
    private Display display = new Display();
    private Menu menu = new Menu();

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
            }
        }
    }

    private void ShowMenu()
    {
        string choice = menu.ShowMainMenu();

        if (choice == "New Game")
        {
            string playerName = menu.AskPlayerName();

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
            AnsiConsole.Clear();

            display.ShowPlayerStats(player);

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]What do you want to do?[/]")
                    .AddChoices(
                        "Explore an area",
                        "Show inventory",
                        "Return to menu"
                    ));

            switch (choice)
            {
                case "Explore an area":
                    Enemy enemy = new Enemy("Gobelin", 50, 5, 50);

                    CombatManager combat = new CombatManager();
                    Item? loot = combat.StartCombat(player, enemy);

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
                    break;

                case "Show inventory":
                    ShowInventory();
                    break;

                case "Return to menu":
                    state = GameState.Menu;
                    break;
            }
        }
    }
    private void ShowInventory()
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(
            new FigletText("Inventory")
                .Centered()
                .Color(Color.Blue));

        List<Item> items = player.Inventory.GetItems();

        if (items.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Inventory is empty.[/]");
            Console.ReadKey();
            return;
        }

        List<string> choices = new List<string>();

        foreach (Item item in items)
        {
            choices.Add(item.Name);
        }

        choices.Add("Back");

        string choice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("[yellow]Choose an item :[/]")
        .AddChoices(choices));

        if (choice == "Back")
            return;

        Item selectedItem = null;

        foreach (Item item in items)
        {
            if (item.Name == choice)
            {
                selectedItem = item;
                break;
            }
        }

        if (selectedItem != null)
        {
            selectedItem.Use(player);
            player.Inventory.RemoveItem(selectedItem);

            AnsiConsole.MarkupLine($"[green]{selectedItem.Name} used![/]");
        }

        Console.ReadKey();
    }
}