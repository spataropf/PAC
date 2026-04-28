using PAC.Interfaces;

namespace PAC.Combat;

public class CriticalAttack : IAttack
{
    private Random random = new Random();

    public int CalculateDamage(int baseAttack)
    {
        if (random.Next(0, 2) == 0)
            return baseAttack * 2;

        return baseAttack;
    }
}