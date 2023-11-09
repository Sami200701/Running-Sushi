using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float accel;
    public float decel;

    public float inAirAccel;

    public float jumpHeight;

    public float gravityScale;
    public float fallGravityScale;

    public float cancelRate;

    public float coyoteTime;
    public float jumpBuffer;

    public LayerMask jumpableGround;

    public bool conserveMomentum;

    private bool facingRight;

    private bool jumping;
    private bool jumpCancelled;

    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private float moveInput;
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = fallGravityScale;

        coll = GetComponent<BoxCollider2D>();

        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        coyoteTimeCounter -= Time.deltaTime;
        jumpBufferCounter -= Time.deltaTime;

        if (moveInput != 0)
        {
            Flip();
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }

        if (Input.GetButtonDown("Jump") && !jumping)
        {
            jumpBufferCounter = jumpBuffer;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
        }

        if (jumping)
        {
            if (Input.GetButtonUp("Jump"))
            {
                jumpCancelled = true;
                coyoteTimeCounter = 0f;
            }

            if (rb.velocity.y < 0f)
            {
                rb.gravityScale = fallGravityScale;
                jumping = false;
            }
        }

    }

    void FixedUpdate()
    {
        // rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        Run();

        if (jumpCancelled && jumping && rb.velocity.y > 0f)
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void Run()
    {
        float tspeed = moveInput * speed;
        float speedDiff = tspeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(tspeed) > 0.01f) ? accel : decel;
        if (!IsGrounded())
        {
            accelRate *= inAirAccel;
        }

        if (conserveMomentum && Mathf.Abs(rb.velocity.x) > Mathf.Abs(tspeed) && rb.velocity.x * tspeed > 0 && !IsGrounded())
        {
            accelRate = 0;
        }

        float movement = speedDiff * accelRate;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private void Jump()
    {
        rb.gravityScale = gravityScale;
        float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2f) * rb.mass;

        if (rb.velocity.y < 0)
        {
            jumpForce -= rb.velocity.y;
        }

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumping = true;
        jumpCancelled = false;
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
    }

    private void Flip()
    {
        if (facingRight && moveInput < 0f || !facingRight && moveInput > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
