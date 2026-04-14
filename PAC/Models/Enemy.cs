namespace PAC.Models;

public class Enemy : Character
{
    public int RewardXp { get; private set; }

    public Enemy(string name, int health, int attack, int rewardXp)
        : base(name, health, attack)
    {
        RewardXp = rewardXp;
    }
}