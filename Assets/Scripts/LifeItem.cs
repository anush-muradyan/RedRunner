using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class LifeItem : MonoBehaviour
    {
        [SerializeField] private float minLife;
        [SerializeField] private float maxLife;
        private float currentAmount;

        private void Awake()
        {
            currentAmount = Random.Range(minLife, maxLife);
            setScale();
        }

        private void setScale()
        {
            var scale = currentAmount.Map(minLife, maxLife, 0.2f, 1.2f);
            transform.localScale = Vector3.one * scale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var life = other.GetComponent<ILifeApply>();

            if (life.CanApply())
            {
                life?.Apply(currentAmount);
                Destroy(gameObject);
            }
        }
    }
}