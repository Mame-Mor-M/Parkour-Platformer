using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JKmovement : MonoBehaviour
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
    [SerializeField] bool isGrounded;
    [SerializeField] Transform feetPos;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float feetRadius;
    [SerializeField] float jumpPower;
    float jumpForce;


    //Jump duration handler
    [SerializeField] float jumpCounter;
    [SerializeField] float jumpDuration;

    public float chargeCounter;
    public float chargeDuration;

    //Wall physics
    [SerializeField] PhysicsMaterial2D bounce, normal;

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

        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRadius, groundLayer);
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRadius, groundLayer);
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jumpForce += jumpPower * Time.deltaTime;

            chargeCounter += Time.deltaTime;
            if (chargeCounter > chargeDuration)
            {
                Jump();
                isGrounded = false;
                isJumping = true;
                chargeCounter = 0;
                jumpForce = 0;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            Jump();
            chargeCounter = 0;
            isJumping = true;
            if (isGrounded)
            {
                jumpForce = 0;
            }
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
        }
        if (!isRunning)
        {
            //rb.MovePosition(rb.position + input * moveSpeed * Time.deltaTime);
            rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y);

            //Prevents the player from moving when space button is held.
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(0 * moveSpeed, rb.velocity.y);
            }
        }
    }

    void Jump()
    {
        //rb.velocity = Vector2.up * jumpForce;
        rb.velocity = new Vector2(input.x, jumpForce);
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
