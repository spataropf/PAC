namespace PAC.Interfaces;

public interface IAttackable
{
    void TakeDamage(int damage);
    bool IsAlive { get; }
}