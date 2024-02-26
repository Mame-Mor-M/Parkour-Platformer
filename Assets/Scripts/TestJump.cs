using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJump : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpHeight;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float colliderRadius;
    bool canJump;
    bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        jumpInput();
    }
    private void FixedUpdate()
    {
        Jump();
    }

    void jumpInput()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, colliderRadius, groundLayer);

        if(!grounded)
        {
            canJump = false;
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            canJump = true;
        }


    }
    void Jump()
    {
        
        if(canJump)
        {
            Debug.Log("Jumped!");
            rb.velocity = Vector2.up * jumpHeight;
        }
    }
}
