using DefaultNamespace.IGameStates;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour, IGamePause, IGameResume
    {
        [SerializeField] private float speed;
        private Vector2 dir;
        private bool pause;

        public void Shoot(Vector2 dir)
        {
            this.dir = dir;
        }

        private void Update()
        {
            transform.Translate(dir * speed * 1.12f * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            Destroy(transform.gameObject);
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