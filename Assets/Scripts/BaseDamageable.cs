using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BaseDamageable : MonoBehaviour, IDamageable
    {
        [Header("Life")] [SerializeField] protected float minLife = 0f;
        [SerializeField] protected float maxLife = 100f;
        public abstract float Life { get; protected set; }
        public bool IsDead => Life <= 0;

        public void TakeDamage(float amount)
        {
            DecreaseLife(amount);
            Debug.Log("Player life " + Life);
        }

        public virtual void Die()
        {
            Debug.LogError("Die");
            Destroy(gameObject);
        }

        public void IncreaseLife(float amount)
        {
            Life += amount;
            if (Life > maxLife)
            {
                Life = maxLife;
            }
        }

        public void DecreaseLife(float amount)
        {
            Life -= amount;
            if (Life <= minLife)
            {
                Life = minLife;
                Die();
            }
        }
        
    }
}