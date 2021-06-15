using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string JUMP_KEY = "Jump";
    private const string BLINK_KEY = "Blink";
    private const string MOVEMENT_BLEND_TREE_KEY = "Movement";

    private void Update()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        animator.SetFloat(MOVEMENT_BLEND_TREE_KEY, inputX);
    }
}


