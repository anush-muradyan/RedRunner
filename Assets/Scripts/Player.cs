using System;
using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour,ILife
{
    [SerializeField] private Animator animator;
    [SerializeField] private float defaultLifeAmount = 100f;
    [SerializeField] private Animation _animation;
    public CharacterController2D controller;
    private Collider2D collider2D;

    public float inputX;
    public float Life { get; set; }

    private void Awake()
    {
        Life = defaultLifeAmount;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _animation.Play("Rotateanim");
        }
        inputX = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", inputX);
        controller.Move(inputX);

        animator.SetBool("IsJumping", !controller.IsGrounded && controller.rigidbody2D.velocity.y > 0f);
        animator.SetBool("IsFalling", controller.rigidbody2D.velocity.y < 0f);
        animator.SetFloat("Velocity", controller.rigidbody2D.velocity.y);

        animator.SetBool("IsMoveing", inputX != 0f);


        if (inputX != 0)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (inputX < 0f ? -1f : 1f);
            transform.localScale = scale;
        }

        /*if (transform.localScale.x > 0f && input < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
        
        if (transform.localScale.x < 0f && input > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
        
        animator.SetFloat(MOVEMENT_BLEND_TREE_KEY, inputX);
        controller.Move(inputX == 0 ? inputX : input);
        
        animator.SetBool(JUMP_KEY, !controller.IsGrounded && !controller.IsFalling);
        animator.SetBool(FALLING_KEY, controller.IsFalling);
*/
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var unit = other.collider.GetComponent<IUnit>();
        if (unit == null)
        {
            return;
        }

        
        // switch (unit)
        // {
        //     case EnemyUnit enemyUnit:
        //         enemyUnit.TakeDamage(this);
        //         Debug.LogError(Life);
        //         break;
        //     default:
        //         throw new ArgumentOutOfRangeException(nameof(unit));
        // }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}