using System;
using System.Collections.Generic;
using UnityEngine;

namespace WayPoints
{
    public enum WayPointType
    {
        Closed,
        Reverse
    }

    [ExecuteInEditMode]
    public class WayPointSystem : MonoBehaviour
    {
        [SerializeField] private WayPointType wayPointType;
        public List<Transform> wayPoints = new List<Transform>();
        private int currentPointIndex;
        private int currentMovementDirection = 1;

        private void updatePoints()
        {
            wayPoints?.Clear();
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                var child = transform.GetChild(i);
                wayPoints.Add(child);
            }
        }

        private void Awake()
        {
            updatePoints();
        }

        private void Update()
        {
            updatePoints();
        }

        private void OnDrawGizmos()
        {
            if (wayPoints == null || wayPoints.Count == 0)
            {
                return;
            }

            Gizmos.color = Color.green;
            for (int i = 0; i < wayPoints.Count; i++)
            {
                Gizmos.DrawSphere(wayPoints[i].position, 0.1f);
                if (i > 0)
                {
                    Gizmos.DrawLine(wayPoints[i - 1].position, wayPoints[i].position);
                }
            }
        }

        public Transform GetCurrentPoint()
        {
            return wayPoints[currentPointIndex];
        }

        public Transform GetNextPoint()
        {
            switch (wayPointType)
            {
                case WayPointType.Closed:
                    return nextPointAsClosed();
                case WayPointType.Reverse:
                    return nextPointAsReverse();
                default:
                    throw new Exception("Lolo");
            }
        }

        private Transform nextPointAsReverse()
        {
            currentPointIndex += currentMovementDirection;
            if (currentPointIndex >= wayPoints.Count)
            {
                currentMovementDirection = -1;
                currentPointIndex += currentMovementDirection;
            }

            if (currentPointIndex < 0)
            {
                currentMovementDirection = 1;
                currentPointIndex += currentMovementDirection;
            }

            return wayPoints[currentPointIndex];
        }

        private Transform nextPointAsClosed()
        {
            currentPointIndex = currentPointIndex++ % wayPoints.Count;
            return wayPoints[currentPointIndex];
        }
    }
}