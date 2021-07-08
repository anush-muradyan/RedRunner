using DefaultNamespace.IGameStates;
using UnityEngine;

namespace WayPoints
{
    public class WayPointMovement : MonoBehaviour,IGamePause,IGameResume
    {
        [SerializeField] private WayPointSystem wayPointSystem;

        [SerializeField] private float movementDuration;
        public bool init;
        private Vector3 currentPoint;
        private Vector3 lastPoint;
        private float speed;
        private float step;
        private bool pause;
        
        private void Update()
        {
            if (pause)
            {
                return;
            }
            
            Move();
        }
        
       

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
            
        }

        public void PauseGame()
        {
            pause = true;
        }

        public void ResumeGame()
        {
            pause = false;
        }
    }
}