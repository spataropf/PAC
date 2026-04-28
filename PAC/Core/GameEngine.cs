using PAC.Models;
using PAC.Combat;
using Spectre.Console;
using PAC.UI;
namespace PAC.Core;

using PAC.World;
using PAC.Services;

public class GameEngine
{
    private Player player;
    private GameState state;
    private Display display = new Display();
    private Menu menu = new Menu();
    private WorldManager world = new WorldManager();
    private EnemyFactory enemyFactory = new EnemyFactory();
    private SaveService saveService = new SaveService();
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
            string playerName = saveService.AskPlayerName();
            player = new Player(playerName);
            state = GameState.Exploration;
        }
        else if (choice == "Load Game")
        {
            Player? loadedPlayer = saveService.Load();

            if (loadedPlayer != null)
            {
                player = loadedPlayer;
                state = GameState.Exploration;
            }
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
                        "Save game",
                        "Return to menu"
                    ));

            switch (choice)
            {
                case "Explore an area":

                    int roll = world.GetRandomEvent();

                    if (roll == 0)
                    {
                        AnsiConsole.MarkupLine("[grey]Nothing happens...[/]");
                        Console.ReadKey();
                    }
                    else if (roll == 1)
                    {
                        Enemy enemy = enemyFactory.CreateRandomEnemy();

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
                    }
                    else
                    {
                        Item potion = new Item("Potion", "Heals 20 HP", 20);
                        player.Inventory.AddItem(potion);

                        AnsiConsole.MarkupLine("[green]You found a potion![/]");
                        Console.ReadKey();
                    }

                    break;

                case "Show inventory":
                    ShowInventory();
                    break;

                case "Save game":
                    saveService.Save(player);
                    AnsiConsole.MarkupLine("[green]Game saved![/]");
                    Console.ReadKey();
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