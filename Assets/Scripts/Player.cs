using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public CharacterController2D controller;
    private Collider2D collider2D;

    private const string JUMP_KEY = "Jump";
    private const string JUMP_INVERSE_KEY = "Jump_inverse";
    private const string BLINK_KEY = "Blink";
    private const string MOVEMENT_BLEND_TREE_KEY = "Movement";
    private const string FALLING_KEY = "Falling";

    public float inputX;

    private void Update()
    {
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Dlflfk");
        transform.position = new Vector3(other.transform.position.x, transform.position.y);
    }
}