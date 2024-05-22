using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJumpPlus : MonoBehaviour
{
    float h;
    public float speed;
    Rigidbody2D rb;

    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayer, wallLayer;
    public bool isGrounded;
    public bool jump;

    //Wall jump stuff
    public Transform wallCheck;
    public bool isWallTouch;
    public bool isSliding;
    public float wallSlidingSpeed;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    public bool wallJumping;
    public bool atApex;
    [SerializeField] float apexGravity;
    [SerializeField] float normalGravity;

    //Jump charge
    [SerializeField] float chargeDuration;
    public float chargeCounter;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(1f, .41f), 0, groundLayer);
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.15f, 1f), 0, wallLayer);

        if (rb.velocity.y <= 0 && !isGrounded)
        {
            atApex = true;
        }
        else
        {
            atApex = false;
        }

        if (isWallTouch && !isGrounded && h != 0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }

        //flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        if (jump)
        {
            Jump();
        }

        if (isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -1 * wallSlidingSpeed, float.MaxValue));
        }

        if (wallJumping)
        {
            rb.velocity = new Vector2(-h * wallJumpForce.x, wallJumpForce.y);
        }

        else
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
        }

        if (atApex)
        {
            rb.gravityScale = apexGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        else if (isSliding)
        {
            wallJumping = true;
            Invoke("StopWallJump", wallJumpDuration);
        }

        jump = false;
    }

    void StopWallJump()
    {
        wallJumping = false;
    }

    void flip()
    {
        if (h < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        if (h < 0.01f) transform.localScale = new Vector3(1, 1, 1);
    }
}
