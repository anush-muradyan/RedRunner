namespace DefaultNamespace
{
    public interface IUnit
    {
    }
    public interface IDamageable
    {
        void TakeDamage(float amount);
        void Die();
    }
}