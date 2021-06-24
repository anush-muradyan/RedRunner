using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] private float damageAmount;

        public void TakeDamage(ILife life)
        {
            life.Life -= damageAmount;
            if (life.Life < 0f)
            {
                life.Die();
            }
        }

    }
}