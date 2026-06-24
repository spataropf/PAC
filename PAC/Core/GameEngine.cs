
namespace PAC.Core;

using PAC.Models;
using PAC.Combat;
using Spectre.Console;
using PAC.UI;
using PAC.World;
using PAC.Services;


public class GameEngine
{
    private Player player;
    private GameState state;
    private Display display = new Display();
    private Menu menu = new Menu();
    private WorldManager worldManager = new WorldManager();
    private EnemyFactory enemyFactory = new EnemyFactory();
    private SaveService saveService = new SaveService();
    private InventoryMenu inventoryMenu = new InventoryMenu();
    private CombatManager combatManager = new CombatManager();
    private ExplorationManager explorationManager = new ExplorationManager();
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
                    explorationManager.Explore(player);
                    break;

                case "Show inventory":
                    inventoryMenu.Show(player);
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
}