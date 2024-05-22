using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedJumpTest : MonoBehaviour
{
    //Component stuffs
    Vector2 input;
    Rigidbody2D rb;
    [SerializeField] Animator animator;

    //Movement stuffs
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    bool isRunning;
    bool facingRight = true;
    bool isMoving;

    //Jumping stuff
    bool isJumping;
    bool isGrounded;
    [SerializeField] Transform feetPos;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float feetRadius;
    [SerializeField] float jumpPower;
    [SerializeField] float jumpHopPower;
    float jumpForce;
    float jumpHopForce;

    //Jump duration handler
    [SerializeField] float jumpCounter;
    [SerializeField] float jumpDuration;

    public float chargeCounter;
    public float chargeDuration;

    //Wall physics
    [SerializeField] PhysicsMaterial2D bounce, normal;

    //Ice Platform
    [SerializeField] LayerMask iceLayer;
    [SerializeField] float decelerationRate;
    bool sliding;

    //Wall Jump stuff
    public bool onWall;
    public bool wallSliding;
    public bool canWallJump;
    [SerializeField] float wallJumpDuration;
    [SerializeField] Transform checkWall;
    [SerializeField] float bodyRadius;
    [SerializeField] Vector2 wallJumpForce;
    [SerializeField] LayerMask wallLayer;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void PlayerInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");

        if (input.x != 0)
        {
            isMoving = true;
        }

        if (input.x == 0)
        {
            isMoving = false;
        }

        //Movement animation stuff.
        animator.SetFloat("movementSpeed", input.sqrMagnitude);

        if (input.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (input.x < 0 && facingRight)
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        //Detects what platform the player is on.
        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRadius, groundLayer);
        sliding = Physics2D.OverlapCircle(feetPos.position, feetRadius, iceLayer);
        onWall = Physics2D.OverlapBox(checkWall.position, new Vector2(0.15f, 1f),0 , wallLayer);

        if (Input.GetKey(KeyCode.Space) && (isGrounded))
        {
            jumpForce += jumpPower * Time.deltaTime;
            jumpHopForce += jumpHopPower * Time.deltaTime;

            chargeCounter += Time.deltaTime;
            if (chargeCounter >= chargeDuration)
            {
                Jump();
                isGrounded = false;
                isJumping = true;
                chargeCounter = 0;
                jumpForce = 0;
            }

            /*if(chargeCounter <= (chargeDuration * 0.2))
            {
                chargeCounter = chargeDuration * 0.25f;
                Jump();
                isGrounded = false;
                isJumping = true;
                chargeCounter = 0;
                jumpForce = 0;
            }*/

        }

        if (Input.GetKeyUp(KeyCode.Space) && (isGrounded))
        {
            /*if(chargeCounter <= (chargeDuration * 0.2))
            {
                chargeCounter = chargeDuration * 0.25f;
                Jump();
                isGrounded = false;
                isJumping = true;
                chargeCounter = 0;
                jumpForce = 0;
            }*/

            /*if(chargeCounter <= 0.2f)
            {
                JumpHop();
                chargeCounter = 0;
                isJumping = true;
                if (isGrounded)
                {
                    jumpHopForce = 0;
                    jumpForce = 0;
                }
            }
            else
            {
                Jump();
                chargeCounter = 0;
                isJumping = true;
                if (isGrounded)
                {
                    jumpForce = 0;
                    jumpHopForce = 0;
                }
            }*/
            Jump();
            chargeCounter = 0;
            isJumping = true;
            if (isGrounded)
            {
                jumpForce = 0;
                jumpHopForce = 0;
            }
        }

        if(onWall && !isGrounded && input.x != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        canWallJump = wallSliding;

        if(Input.GetKeyDown(KeyCode.Space) && wallSliding)
        {
            WallJump();
            Invoke("StopWallJump", wallJumpDuration);
        }

        if (jumpForce <= 0 && !isGrounded)
        {
            rb.sharedMaterial = bounce;
        }
        /*if (jumpForce > 0)
        {
            rb.sharedMaterial = bounce;
        }*/
        else
        {
            rb.sharedMaterial = normal;
        }
    }
    void Move()
    {
        if (isRunning)
        {
            //rb.MovePosition(rb.position + input * runSpeed * Time.deltaTime);
            rb.velocity = new Vector2(input.x * runSpeed, rb.velocity.y);

            if(sliding)
            {
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    float moveH = Input.GetAxis("Horizontal");
                    Vector2 movement = new Vector2(moveH, 0);
                    rb.velocity = movement.normalized * runSpeed;
                }
                else
                {
                    if (rb.velocity.x > 0.5f || rb.velocity.x < -0.5f)
                    {
                        rb.velocity *= decelerationRate;
                    }
                    else
                    {
                        rb.velocity *= 0;
                    }

                }
            }
        }

        if (!isRunning)
        {
            //rb.MovePosition(rb.position + input * moveSpeed * Time.deltaTime);
            if(!sliding)
            {
                rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y);
            }
            
            if(sliding)
            {
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    float moveH = Input.GetAxis("Horizontal");
                    Vector2 movement = new Vector2(moveH, 0);
                    rb.velocity = movement.normalized * moveSpeed;
                }
                else
                {
                    if (rb.velocity.x > 0.5f || rb.velocity.x < -0.5f)
                    {
                        rb.velocity *= decelerationRate;
                    }
                    else
                    {
                        rb.velocity *= 0;
                    }

                }
            }
            //Prevents the player from moving when space button is held.
            if (Input.GetKey(KeyCode.Space))
            {
                //rb.velocity = new Vector2(0 * moveSpeed, rb.velocity.y);
                rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y);
            }
        }
    }

    void Jump()
    {
        //rb.velocity = Vector2.up * jumpForce;
        rb.velocity = new Vector2(input.x, jumpForce);
    }

    void JumpHop()
    {
        rb.velocity = new Vector2(input.x, jumpHopForce);
    }

    void WallJump()
    {
        rb.velocity = new Vector2(-input.x * wallJumpForce.x, wallJumpForce.y);
    }

    void StopWallJump()
    {
        canWallJump = false;
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}