namespace PAC.Models;

public abstract class Character
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Attack { get; private set; }

    public bool IsAlive => Health > 0;

    public Character(string name, int health, int attack)
    {
        Name = name;
        MaxHealth = health;
        Health = health;
        Attack = attack;
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0)
            Health = 0;
    }

    public void Heal(int amount)
    {
        Health += amount;

        if (Health > MaxHealth)
            Health = MaxHealth;
    }

    public void IncreaseMaxHealth(int amount)
    {
        MaxHealth += amount;
        Health = MaxHealth;
    }
}