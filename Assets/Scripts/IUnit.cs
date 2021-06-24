namespace DefaultNamespace
{
    public interface IUnit
    {
        void TakeDamage(ILife life);
    }

    public interface ILife
    {
        float Life { get; set; }

        void Die();
    }
}