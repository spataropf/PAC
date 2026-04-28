using Spectre.Console;

namespace PAC.UI;

public class Menu
{
    public string ShowMainMenu()
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(
            new FigletText("Mini RPG")
                .Centered()
                .Color(Color.Green));

        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Choose an option :[/]")
                .AddChoices("New Game", "Load Game", "Quit"));
    }
}