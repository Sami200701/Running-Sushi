using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement pmov;
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

        if (pmov.moveInput != 0)
        {
            animator.SetBool("IsRunning", true);//running animation
        }
        else animator.SetBool("IsRunning", false);//idle animation

        animator.SetBool("IsJumping", pmov.jumping);

    }
}
