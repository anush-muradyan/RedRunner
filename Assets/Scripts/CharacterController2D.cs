using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D rigidbody2D;
        [SerializeField] private float speed;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float groundCollisionMinDistance;


        public bool IsGrounded { get; private set; }
        public bool IsFalling => rigidbody2D.velocity.y < -0.18f;

        [SerializeField] public float jumpforce;

        public void Move(float move)
        {
            var velocity = move * speed;
            var vel = rigidbody2D.velocity;
            vel.x = velocity;
            rigidbody2D.velocity = vel;
        }



        private void Update()
        {
            checkGround();
            Jump();
            // Debug.LogError("Y " + rigidbody2D.velocity.y);
        }

        private void checkGround()
        {
            var hitLeft = Physics2D.Raycast(transform.position - Vector3.right * 0.5f - Vector3.right * 0.05f,
                Vector2.down, groundCollisionMinDistance, groundMask);
            var hitRigth = Physics2D.Raycast(transform.position + Vector3.right * 0.5f - Vector3.right * 0.05f,
                Vector2.down, groundCollisionMinDistance, groundMask);

            IsGrounded = (hitLeft.collider != null) || (hitRigth.collider != null);
            Debug.DrawRay(transform.position - Vector3.right * 0.4f, Vector2.down * groundCollisionMinDistance,
                Color.magenta);
            Debug.DrawRay(transform.position + Vector3.right * 0.4f, Vector2.down * groundCollisionMinDistance,
                Color.magenta);
        }

        private void Jump()
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded)
            {
                rigidbody2D.AddForce(jumpforce * Vector3.up, ForceMode2D.Impulse);
            }

        }

    }
}
