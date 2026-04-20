using Spectre.Console;
using PAC.Models;

namespace PAC.UI;

public class Display
{
    public void ShowPlayerStats(Player player)
    {
        AnsiConsole.Write(
            new Panel($"[green]{player.Name}[/]\nHP : {player.Health}/{player.MaxHealth}")
                .Header("Statistics")
                .Border(BoxBorder.Rounded));
    }
}