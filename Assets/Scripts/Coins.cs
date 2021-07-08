using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private Player player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player.coinCount++;
        Debug.Log("Coin count: " +  player.coinCount);
        Destroy(gameObject);
    }
}
