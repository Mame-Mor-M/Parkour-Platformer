using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    [Header("References:\n")]
    public Camera mainCamera;
    public LineRenderer rope;
    public Rigidbody2D rb;

    [Header("Swing Parameters:\n")]
    public bool isSwinging;
    public DistanceJoint2D swingDistance;
    [SerializeField] float swingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        swingDistance.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 6)
            {
                if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.L))
                {
                    isSwinging = true;
                    Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

                    rope.SetPosition(0, mousePos);
                    rope.SetPosition(1, transform.position);
                    swingDistance.connectedAnchor = mousePos;
                    swingDistance.enabled = true;
                    rope.enabled = true;
                    rb.gravityScale = 0;
                    rb.velocity = new Vector2(swingSpeed, rb.velocity.y);
                }
                else if (Input.GetMouseButtonUp(2) || Input.GetKeyUp(KeyCode.L))
                {
                    isSwinging = false;
                    swingDistance.enabled = false;
                    rope.enabled = false;
                    rb.gravityScale = 2;
                }
                if (swingDistance.enabled)
                {
                    rope.SetPosition(1, transform.position);
                }
            }
        }
        
        else
        {
            Debug.Log("No collider hit.");
        }

        
        

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isSwinging = false;
        swingDistance.enabled = false;
        rope.enabled = false;
        rb.gravityScale = 2;
    }
}
