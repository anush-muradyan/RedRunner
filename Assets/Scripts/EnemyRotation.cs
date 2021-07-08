using System;
using DefaultNamespace.IGameStates;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyRotation : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private void Update()
        {
           transform.Rotate(0, 0, speed * Time.deltaTime, Space.Self);
        }
        
    }
}