using PAC.Interfaces;

namespace PAC.Combat;

public class NormalAttack : IAttack
{
    public int CalculateDamage(int baseAttack)
    {
        return baseAttack;
    }
}