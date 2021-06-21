using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public CharacterController2D controller;
    private Collider2D collider2D;

    private const string JUMP_KEY = "Jump";
    private const string BLINK_KEY = "Blink";
    private const string MOVEMENT_BLEND_TREE_KEY = "Movement";
    private const string FALLING_KEY = "Falling";

    private float inputX;
    private bool touching;

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        if (inputX==0f)
        {
            animator.SetFloat(BLINK_KEY,inputX);      
        }
        animator.SetFloat(MOVEMENT_BLEND_TREE_KEY, inputX);
        controller.Move(inputX);
        //animator.SetBool(FALLING_KEY, !controller.IsGrounded);
        
        animator.SetBool(JUMP_KEY,!controller.IsGrounded);
        
        
        
    }
}