using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        player.GetComponent<PlayerMovement>().doubleJump=true;
        Destroy(gameObject);
    }
}
