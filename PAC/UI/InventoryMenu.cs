namespace PAC.UI;

using PAC.Models;
using Spectre.Console;

public class InventoryMenu
{
    public void Show(Player player)
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

        Item? selectedItem = null;

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