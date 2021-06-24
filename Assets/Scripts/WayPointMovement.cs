using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DefaultNamespace
{
    public class WayPointMovement : MonoBehaviour
    {
        [SerializeField] private WayPointSystem wayPointSystem;

        [SerializeField] private float movementDuration;
        public bool init;
        private Vector3 currentPoint;
        private Vector3 lastPoint;
        private float speed;
        private float step;
        
        
        private void Update()
        {
            Move();
        }
        

        private Stopwatch _stopwatch;

        private void Move()
        {
            if (!init)
            {
                transform.position = wayPointSystem.GetCurrentPoint().position;
                currentPoint = wayPointSystem.GetNextPoint().position;
                init = true;
            }

            if (Vector3.Distance(transform.position, currentPoint) <= 0.25f)
            {
                lastPoint = currentPoint;
                currentPoint = wayPointSystem.GetNextPoint().position;
            }

            var d1 = Vector3.Distance(lastPoint, transform.position);
            var d = Vector3.Distance(lastPoint, currentPoint);
            var t = (d1 / d) + 0.01f;
            
            transform.position = Vector3.Lerp(transform.position, currentPoint, t * Time.deltaTime );


            // if (currentPoint == null || )
            // {
            //     if (_stopwatch == null)
            //     {
            //         _stopwatch = new Stopwatch();
            //         _stopwatch.Start();
            //     }
            //
            //     if (_stopwatch != null)
            //     {
            //         _stopwatch.Stop();
            //         Debug.LogError(_stopwatch.ElapsedMilliseconds);
            //         _stopwatch = new Stopwatch();
            //         _stopwatch.Start();
            //     }
            //   
            //     currentPoint = wayPointSystem.GetNextPoint();
            //     speed = wayPointSystem.GetDistance() / movementDuration;
            //     Debug.Log(speed);
            //     
            // }
            // step = speed * Time.deltaTime;
            // transform.position = Vector3.Lerp(transform.position, currentPoint.position, step);
        }
    }
}