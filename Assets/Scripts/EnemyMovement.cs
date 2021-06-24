using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyMovement:MonoBehaviour
    {
        [SerializeField] private float speed;
        private void Update()
        {
            transform.Rotate(0, 0, speed * Time.deltaTime,  Space.Self);
        }
    }
}