using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] public float damageAmount;

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            var damageable = other.collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(damageAmount);
        }
    }

    public abstract class DamageableUnit : BaseDamageable, IUnit
    {
        [SerializeField] public float damageAmount;

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            var damageable = other.collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(damageAmount);
        }
    }
}