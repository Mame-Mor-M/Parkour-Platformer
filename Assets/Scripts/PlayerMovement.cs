using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] float regSpeed;
    [SerializeField] float slowSpeed;
    bool isRunning;
    bool facingRight = true;

    //Jumping stuffs
    bool grounded;
    bool jumping;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform checkFeet;
    [SerializeField] float feetColliderRadius;
    [SerializeField] float downForce;

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

    //Swinging stuffs
    [SerializeField] Swinging swingScript;

    //Other stuffs
    [SerializeField] Transform throwPos;
    [SerializeField] float regThrowY;
    [SerializeField] float crouchThrowY;

    //Particle stuffs
    [SerializeField] ParticleSystem effects;

    public AudioSource hurtSound;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        regThrowY = throwPos.position.y;
        crouchThrowY = throwPos.position.y * 1.4f;
        regSpeed = moveSpeed;
        canAttack = true;
        


    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
        //JumpInputs();
        rb.gravityScale = 3;


    }

    void FixedUpdate()
    {
        Jump();
        Move();

    }

    void PlayerInputs()
    {
        Collider2D[] swingRadius = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        input.x = Input.GetAxisRaw("Horizontal");

        //Movement animation stuffs.
        animator.SetFloat("movementSpeed", runSpeed);
        if (input.x > 0 && !facingRight)
        {
            Flip();

        }

        else if (input.x < 0)
        {
            moveSpeed = slowSpeed;
        }
        else
        {
            moveSpeed = regSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.D)))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        grounded = Physics2D.OverlapCircle(checkFeet.position, feetColliderRadius, groundLayer);

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && grounded)
        {
            animator.SetBool("isSlide", false);
            jumping = true;
            jumpTimeCounter = jumpTime;
        }
        if ((Input.GetButton("Jump") || Input.GetKey(KeyCode.W)) && jumping)
        {
            animator.SetBool("isSlide", false);
            if (jumpTimeCounter > 0)
            {
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }
        if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.W))
        {
            jumping = false;
        }

        if(input.x != 0 && grounded)
        {
            MovingEffects();
        }

        //Attacking animation.
        attackTime = attackRate;
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J)) && canAttack == true)
        {
            animator.SetBool("isSlide", false);
            animator.SetTrigger("Attack");
            StartCoroutine("swordSwing");
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger("Attack Stopped");
        }
        attackTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.C) || (Input.GetKeyDown(KeyCode.S)) && !isSliding && grounded)
        {
            Slide();
        }

        if (Input.GetKeyDown(KeyCode.S) && !grounded)
        {
            forceDown();
        }
    }


    void Move()
    {
        if (isRunning && swingScript.isSwinging == false)
        {
            //rb.MovePosition(rb.position + input * runSpeed * Time.deltaTime);
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        }
        if (!isRunning && swingScript.isSwinging == false)
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

    void forceDown()
    {
        rb.velocity = Vector2.down * downForce;
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
        canAttack = false; // Prevents sword swing while sliding

        animator.SetBool("isSlide", true);

        regCollider.enabled = false;
        sr.enabled = false;
        slideCollider.enabled = true;
        slideRenderer.enabled = true; // Switch to sliding stance

        throwPos.position = new Vector2(throwPos.position.x, crouchThrowY);

        rb.AddForce(Vector2.right * slideSpeed);
        StartCoroutine("stopSlide"); // Begin 'stop slide' event 
    }

    void MovingEffects()
    {
        effects.Play();
    }

    IEnumerator stopSlide()
    {
        
        yield return new WaitForSeconds(0.8f); // Stop slide after 0.95 seconds

        animator.SetBool("isSlide", false);
        regCollider.enabled = true;
        sr.enabled = true;
        slideCollider.enabled = false;
        slideRenderer.enabled = false;  // Return to default stance
        throwPos.position = new Vector2(throwPos.position.x, regThrowY);
        canAttack = true; // Re-enables sword swing
        yield return new WaitForSeconds(0.8f); // 0.8 second slide cooldown
        isSliding = false;

    }

    IEnumerator swordSwing()
    {
        Collider2D[] swingRadius = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);


        yield return new WaitForSeconds(0.15f);
        foreach (Collider2D collider in swingRadius)
        {

            if (collider.CompareTag("Normal"))
            {
                Destroy(collider.gameObject);
            }

        }
        yield return new WaitForSeconds(0.15f);
        animator.SetTrigger("Attack Stopped");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("Laser") || trig.gameObject.CompareTag("Normal"))
        {
            hurtSound.Play();
        }
    }
}