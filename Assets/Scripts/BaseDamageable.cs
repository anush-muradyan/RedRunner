using System;
using DefaultNamespace.IGameStates;
using DefaultNamespace.UI;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BaseDamageable : MonoBehaviour, IDamageable,IDeath
    {
        [Header("Life")] 
        [SerializeField] protected float minLife = 0f;
        [SerializeField] protected float maxLife = 100f;
        public abstract float Life { get; protected set; }
        public bool IsDead => Life <= 0;

        public event Action OnDeath;

        public void TakeDamage(float amount)
        {
            DecreaseLife(amount);
        }

        public virtual void Die()
        {
            OnDeath?.Invoke();
            gameObject.SetActive(false);
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