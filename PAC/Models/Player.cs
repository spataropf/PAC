namespace PAC.Models;

public class Player : Character
{
    public int Level { get; set; }
    public int Experience { get; set; }
    public Inventory Inventory { get; set; }
    public Player() : base("", 0, 0)
    {
        Inventory = new Inventory();
    }

    public Player(string name) : base(name, 100, 10)
    {
        Level = 1;
        Experience = 0;
        Inventory = new Inventory();
    }

    public void GainExperience(int amount)
    {
        Experience += amount;

        if (Experience >= 100)
        {
            LevelUp();
            Experience = 0;
        }
    }

    private void LevelUp()
    {
        Level++;
        IncreaseMaxHealth(10);
    }
}