using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    //Component stuffs
    Vector2 input;
    Rigidbody2D rb;
    [SerializeField] Animator animator;
    SpriteRenderer sr;

    //Movement stuffs
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    bool isRunning;
    bool facingRight = true;

    //Jumping stuffs
    bool grounded;
    bool jumping;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform checkFeet;
    [SerializeField] float feetColliderRadius;

    //Jump duration handler
    [SerializeField] float jumpTime;
    float jumpTimeCounter;

    //Attack stuffs
    [SerializeField] float attackRate;
    [SerializeField] float attackTime;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    bool canAttack;
    [SerializeField] LayerMask playerLayers;

    //Sliding stuffs
    [SerializeField] float slideSpeed;
    [SerializeField] BoxCollider2D regCollider;
    [SerializeField] BoxCollider2D slideCollider;
    [SerializeField] SpriteRenderer slideRenderer;
    private bool isSliding;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
        //JumpInputs();
    }

    void FixedUpdate()
    {
        Jump();
        Move();

    }

    void PlayerInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");

        //Movement animation stuffs.
        animator.SetFloat("movementSpeed", runSpeed);
        if (input.x > 0 && !facingRight)
        {
            Flip();
        }

        else if (input.x < 0)
        {
            moveSpeed = 1.2f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        grounded = Physics2D.OverlapCircle(checkFeet.position, feetColliderRadius, groundLayer);
        /*if(!grounded)
        {
            jumping = false;
        }*/
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
            jumpTimeCounter = jumpTime;
        }
        if (Input.GetButton("Jump") && jumping)
        {
            if (jumpTimeCounter > 0)
            {
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumping = false;
        }

        //Attacking animation.
        attackTime = attackRate;
        if (Input.GetMouseButtonDown(0))
        {
            if (attackTime > 0)
            {
                canAttack = false;
            }
            animator.SetTrigger("Attack");
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger("Attack Stopped");
        }
        attackTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.C) && !isSliding)
        {
            Slide();
        }
    }


    void Move()
    {
        if (isRunning)
        {
            //rb.MovePosition(rb.position + input * runSpeed * Time.deltaTime);
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        }
        if (!isRunning)
        {
            //rb.MovePosition(rb.position + input * moveSpeed * Time.deltaTime);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (jumping)
        {
            Debug.Log("Jumped!");
            rb.velocity = Vector2.up * jumpForce;
            //rb.MovePosition(rb.position + input * jumpForce * Time.deltaTime);
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce) * Time.deltaTime;
        }

    }

    void Attack()
    {

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void Slide()
    {
        isSliding = true;

        animator.SetBool("isSlide", true);

        regCollider.enabled = false;
        sr.enabled = false;
        slideCollider.enabled = true;
        slideRenderer.enabled = true; // Switch to sliding stance


        rb.AddForce(Vector2.right * slideSpeed);
        StartCoroutine("stopSlide"); // Begin 'stop slide' event 
    }

    IEnumerator stopSlide()
    {
        
        yield return new WaitForSeconds(0.8f); // Stop slide after 0.8 seconds
        
        animator.SetBool("isSlide", false);
        regCollider.enabled = true;
        sr.enabled = true;
        slideCollider.enabled = false;
        slideRenderer.enabled = false;  // Return to default stance

        yield return new WaitForSeconds(0.8f); // 0.8 second slide cooldown
        isSliding = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
