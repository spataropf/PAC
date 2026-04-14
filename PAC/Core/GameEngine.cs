using PAC.Models;

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
        Console.Clear();
        Console.WriteLine("=== mini RPG ===");
        Console.WriteLine("1. New Game");
        Console.WriteLine("2. Quit");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Player Name: ");
            string playerName = Console.ReadLine();

            player = new Player(playerName);
            state = GameState.Exploration;
        }
        else if (choice == "2")
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
                    Console.WriteLine("You are exploring a mysterious area...");
                    Console.WriteLine("But nothing happens yet.");
                    Console.ReadKey();
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