using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeathZone:MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private GameManager gameManager;
        private void Update()
        {
            var pos = transform.position;
            pos.x = player.transform.position.x;
            transform.position = pos;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            player.Die();
            other.gameObject.SetActive(false);
        }
    }
}