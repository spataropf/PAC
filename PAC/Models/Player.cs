namespace PAC.Models;

public class Player : Character
{
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public Inventory Inventory { get; private set; }

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