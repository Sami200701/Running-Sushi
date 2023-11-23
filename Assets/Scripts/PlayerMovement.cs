using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool dash;
    public bool doubleJump;
    public bool wallJump;

    public float speed;
    public float accel;
    public float decel;

    public float inAirAccel;

    public float jumpHeight;

    public float gravityScale;
    public float fallGravityScale;

    public float cancelRate;

    public float wallslideSpeed;
    public float wallJumpForce;
    public float wallJumpTime;
    public float wallLerp;

    public float coyoteTime;
    public float jumpBuffer;

    public LayerMask jumpableGround;

    public float dashPower;
    public float dashTime;
    public float dashCooldown;

    public bool conserveMomentum;

    private bool facingRight;

    private bool jumping;
    private bool jumpCancelled;
    private bool canDoubleJump;

    private bool wallSlide;
    private bool wallJumping;

    private bool dashing;
    private bool canDash;

    private float dashCooldownCounter;
    private float dashBufferCounter;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float wallJumpCounter;

    private float moveInput;
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = fallGravityScale;

        coll = GetComponent<BoxCollider2D>();

        facingRight = true;
        canDash = dash;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashing)
        {
            return;
        }

        moveInput = Input.GetAxis("Horizontal");

        coyoteTimeCounter -= Time.deltaTime;
        jumpBufferCounter -= Time.deltaTime;
        dashCooldownCounter -= Time.deltaTime;
        dashBufferCounter -= Time.deltaTime;
        wallJumpCounter -= Time.deltaTime;

        if (moveInput != 0)
        {
            Flip();
        }

        if (IsGrounded())
        {
            canDoubleJump = doubleJump;
        }

        if (IsGrounded() || wallSlide)
        {
            coyoteTimeCounter = coyoteTime;

            if (dashCooldownCounter < 0f)
            {
                canDash = dash;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBuffer;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
        }
        else if (jumpBufferCounter > 0f && canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
        }

        if (jumping)
        {
            if (Input.GetButtonUp("Jump"))
            {
                jumpCancelled = true;
                coyoteTimeCounter = 0f;
            }
        }

        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = fallGravityScale;
            // dashCooldownCounter = 0;
            jumping = false;
        }


        if (Input.GetButtonDown("Dash") && canDash)
        {
            dashBufferCounter = jumpBuffer;
        }

        if (dashBufferCounter > 0f)
        {
            StartCoroutine(Dash());
        }

        if (wallJumping && wallJumpCounter < 0)
        {
            wallJumping = false;
        }

        WallSlide();

    }

    void FixedUpdate()
    {
        if (dashing)
        {
            return;
        }

        if (wallJumping)
        {
            Run(wallLerp);
        }
        else
        {
            Run(1f);
        }


        if (jumpCancelled && jumping && rb.velocity.y > 0f)
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private bool IsWall()
    {
        Vector2 dir = (facingRight) ? Vector2.right : Vector2.left;
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, dir, 0.1f, jumpableGround);
    }

    private void Run(float lerp)
    {
        animator.SetBool("IsRunning", true);//////////aqui puse el IsRunning(pero no se si seria aqui)

        float tspeed = moveInput * speed;
        tspeed = Mathf.Lerp(rb.velocity.x, tspeed, lerp);
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

        if (wallSlide)
        {
            WallJump();
        }

        jumping = true;
        jumpCancelled = false;
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
    }

    private void WallJump()
    {
        Vector2 dir = (facingRight) ? Vector2.left : Vector2.right;
        rb.AddForce(dir * wallJumpForce, ForceMode2D.Impulse);
        wallJumping = true;
        canDoubleJump = false;
        wallJumpCounter = wallJumpTime;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        dashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (facingRight) {
            rb.velocity = Vector2.right * dashPower;
        }
        else
        {
            rb.velocity = Vector2.left * dashPower;
        }
        // tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        // tr.emitting = false;
        rb.gravityScale = originalGravity;
        dashing = false;
        dashCooldownCounter = dashCooldown;
        dashBufferCounter = 0;
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

    private void WallSlide()
    {
        if (wallJump && IsWall() && !IsGrounded() && moveInput != 0f)
        {
            wallSlide = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallslideSpeed, float.MaxValue));
        }
        else
        {
            wallSlide = false;
        }
    }
}
