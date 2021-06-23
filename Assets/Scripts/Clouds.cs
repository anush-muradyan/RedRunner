using System;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField]private float moveSpeed;
    [SerializeField]private float changeDirectionSeconds;
    private float time;
    private void Start()
    {
        time = changeDirectionSeconds;
    }

    private void Update()
    {
        if (Time.time >= changeDirectionSeconds)
        {
            moveSpeed = -moveSpeed;
            changeDirectionSeconds += time;
        }
        transform.Translate((moveSpeed * Time.deltaTime * Vector2.right));
    }

    
}
