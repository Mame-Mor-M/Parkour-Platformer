using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJump : MonoBehaviour
{
    float input;
    public float speed;
    Rigidbody2D rb;
    [SerializeField] Animator animator;

    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer, wallLayer;
    public bool isGrounded;
    public bool jump;

    //Wall jump stuff
    [SerializeField] Transform wallCheck;
    public bool isWallTouch;
    public bool isSliding;
    [SerializeField] float wallSlidingSpeed;
    [SerializeField] Vector2 wallJumpForce;
    public bool wallJumping;
    public bool atApex;
    [SerializeField] float apexGravity;
    [SerializeField] float normalGravity;

    public float wallJumpingDirection;
    public float wallJumpingTime = 0.2f;
    public float wallJumpingCounter;
    [SerializeField] float wallJumpDuration = 0.4f;

    //Orientation
    bool facingRight = true;

    //Teleport back to last checkpoint.
    [SerializeField] Transform start, checkPoint1, checkPoint2;

    //Particles
    [SerializeField] ParticleSystem effects;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        input = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("movementSpeed", input);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.1f, .41f), 0, groundLayer);
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.15f, 1f), 0, wallLayer);

        if (input != 0 && isGrounded)
        {
            MovementEffects();
        }

        if (rb.velocity.y <= 0 && !isGrounded)
        {
            atApex = true;
        }
        else
        {
            atApex = false;
        }

        if(isWallTouch && !isGrounded/* && input != 0*/)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if(jump)
        {
            Jump();
            //MovementEffects();
        }

        if (isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -1 * wallSlidingSpeed, float.MaxValue));
        }

        if(wallJumping)
        {
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpForce.x, wallJumpForce.y);
        }

        else
        {
            rb.velocity = new Vector2(input * speed, rb.velocity.y);
        }

        if(atApex)
        {
            //Debug.Log("Apex = " + new Vector2(0, gameObject.transform.position.y));
            rb.gravityScale = apexGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

    void Jump()
    {
        if(isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce ), ForceMode2D.Impulse);
        }

        else if(isSliding)
        {
            wallJumping = true;
            wallJumpingDirection = -transform.localScale.x;
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
        if (input < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        if (input < 0.01f) transform.localScale = new Vector3(1, 1, 1);
    }

    void Flip()
    {
        
        if(facingRight && input < 0f || !facingRight && input > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void MovementEffects()
    {
        effects.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Spike1"))
        {
            //gameObject.transform.position = start.transform.position;
            StartCoroutine("TeleportStart");
        }
        if (collision.collider.CompareTag("Spike2"))
        {
            //gameObject.transform.position = checkPoint1.transform.position;
            StartCoroutine("TeleportCheckpoint1");
        }
        if(collision.collider.CompareTag("Spike3"))
        {
            //gameObject.transform.position = checkPoint2.transform.position;
            StartCoroutine("TeleportCheckpoint2");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spike1"))
        {
            /*animator.SetBool("isDeath", true);
            gameObject.transform.position = start.transform.position;*/
            StartCoroutine("TeleportStart");
        }
        if (collision.CompareTag("Spike2"))
        {
            /*animator.SetBool("isDeath", true);
            gameObject.transform.position = checkPoint1.transform.position;*/
            StartCoroutine("TeleportCheckpoint1");
        }
        if (collision.CompareTag("Spike3"))
        {
            /*animator.SetBool("isDeath", true);
            gameObject.transform.position = checkPoint2.transform.position;*/
            StartCoroutine("TeleportCheckpoint2");
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike1"))
        {
            animator.SetBool("isDeath", false);
        }
        if (collision.CompareTag("Spike2"))
        {
            animator.SetBool("isDeath", false);   
        }
        if (collision.CompareTag("Spike3"))
        {
            animator.SetBool("isDeath", false);
        }
    }*/
    IEnumerator TeleportStart()
    {
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.position = start.transform.position;
        animator.SetBool("isDeath", false);
    }
    IEnumerator TeleportCheckpoint1()
    {
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.position = checkPoint1.transform.position;
        animator.SetBool("isDeath", false);
    }
    IEnumerator TeleportCheckpoint2()
    {
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.position = checkPoint2.transform.position;
        animator.SetBool("isDeath", false);
    }
}
