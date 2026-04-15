using PAC.Core;
using Spectre.Console;

namespace PAC;

public class Program
{
    public static void Main(string[] args)
    {
        GameEngine game = new GameEngine();
        game.Start();
    }
}