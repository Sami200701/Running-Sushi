using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int pupCode;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        switch (pupCode)
        {
            case(0):
                player.GetComponent<PlayerMovement>().doubleJump=true;
                break;
            case(1):
                player.GetComponent<PlayerMovement>().wallJump=true;
                break;
            default:
                player.GetComponent<PlayerMovement>().dash=true;
                break;
        }
        Destroy(gameObject);
    }
}
