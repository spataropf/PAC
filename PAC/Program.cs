using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Bienvenue ![/]");

var nom = AnsiConsole.Ask<string>("Quel est ton [green]nom[/] ?");

var choix = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Que veux-tu faire ?")
        .AddChoices("Jouer", "Quitter"));

if (choix == "Jouer")
{
    AnsiConsole.MarkupLine($"[yellow]Bienvenue {nom} dans le jeu ![/]");
}
else
{
    AnsiConsole.MarkupLine("[red]Au revoir ![/]");
}