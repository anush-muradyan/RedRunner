using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class WayPointMovement : MonoBehaviour
    {
        [SerializeField] private WayPointSystem wayPointSystem;
        [SerializeField] private float speed;
        private Transform currentPoint;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (currentPoint == null || Vector3.Distance(transform.position,currentPoint.position)<0.01f)
            {
                currentPoint = wayPointSystem.GetNextPoint();
            }

            transform.position = Vector3.Lerp(transform.position, currentPoint.position, 0.05f );
        }
    }
}