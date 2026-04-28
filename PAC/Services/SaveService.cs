using PAC.Models;
using System.Text.Json;

namespace PAC.Services;

public class SaveService
{
    private string path = "save.json";

    public void Save(Player player)
    {
        string json = JsonSerializer.Serialize(player);
        File.WriteAllText(path, json);
    }

    public Player? Load()
    {
        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);

        Player? player = JsonSerializer.Deserialize<Player>(json);

        if (player != null && player.Inventory == null)
        {
            player.Inventory = new Inventory();
        }

        return player;
    }
}