using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PowerUp : MonoBehaviour
{
    public int pupCode; //0: doubleJump, 1: wallJump, 2: dash
    private GameMaster gm;
    private PlayerMovement playerMov;

    public GameObject pickupEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other.GetComponent<PlayerMovement>());
        }
    }

    void Pickup(PlayerMovement player)
    {
        switch (pupCode)
        {
            case(0):
                player.GetComponent<PlayerMovement>().doubleJump=true;
                gm.preDoubleJump = true;
                break;
            case(1):
                player.GetComponent<PlayerMovement>().wallJump=true;
                gm.preWallJump = true;
                break;
            default:
                player.GetComponent<PlayerMovement>().dash=true;
                gm.preDash = true;
                break;
        }

        Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        switch (pupCode)
        {
            case(0):
                if (gm.doubleJump)
                {
                    Pickup(playerMov);
                }
                break;
            case(1):
                if (gm.wallJump)
                {
                    Pickup(playerMov);
                }
                break;
            default:
                if (gm.dash)
                {
                    Pickup(playerMov);
                }
                break;
        }
    }

    private void Update()
    {
        transform.Rotate(0, .3f, 0);
    }
}
