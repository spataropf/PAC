using PAC.Interfaces;

namespace PAC.Combat;

public class CriticalAttack : IAttack
{
    public int CalculateDamage(int baseAttack)
    {
        return baseAttack * 2;
    }
}