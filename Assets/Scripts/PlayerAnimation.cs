using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement pmov;
    
    private bool corriendoSonando = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pmov = GetComponent<PlayerMovement>();

        animator.SetBool("IsGrounded", true);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsGrounded", pmov.IsGrounded());
        if (pmov.moveInput != 0 && pmov.IsGrounded())
        {
            animator.SetBool("IsRunning", true);//running animation
            if (!corriendoSonando)
            {
                corriendoSonando = true;
                FindObjectOfType<AudioManager>().Play("Running");
            }
        }
        else
        {
            corriendoSonando = false;
            animator.SetBool("IsRunning", false); //idle animation
            FindObjectOfType<AudioManager>().Stop("Running");
        }
    

        animator.SetBool("IsJumping", pmov.jumping);
        if ((Input.GetButtonDown("Jump") && pmov.IsGrounded()) || (Input.GetButtonDown("Jump") && !pmov.IsGrounded() && pmov.doubleJump) || (pmov.autoDoubleJump && Input.GetButton("Jump") && !pmov.jumping))
        {
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        
    }
}
