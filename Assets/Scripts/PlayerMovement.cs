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

    private bool grounded;
    private bool jumping;
    private bool jumpCancelled;

    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private float x;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");

        if (grounded)
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }

    }

    // private bool IsGrounded()
    // {
    //     return Physics.OverlapBox(gc.position, gc.localScale/2, Quaternion.identity, gl);
    // }
}
