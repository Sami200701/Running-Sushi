using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpHeight;

    public float gravityScale;
    public float fallGravityScale;

    public float cancelRate;

    public float coyoteTime;
    public float jumpBuffer;

    public LayerMask jumpableGround;

    private bool jumping;
    private bool jumpCancelled;

    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private float x;
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;

        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && !jumping)
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.gravityScale = gravityScale;
            float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2f) * rb.mass;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
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
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        if (jumpCancelled && jumping && rb.velocity.y > 0f)
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
