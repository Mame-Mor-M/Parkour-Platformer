using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatformTest : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float ms = 5;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask iceLayer;
    public bool sliding;
    [SerializeField] float decelerationRate;
    float lastMovementValue;


    private void Update()
    {

        //float h = Input.GetAxis("Horizontal");
        sliding = Physics2D.OverlapCircle(player.position, 3f, iceLayer);


        if(!sliding)
        {
            //rb.velocity = new Vector2(ms * h, 0);
        }

        if(sliding)
        {
            if(Input.GetAxisRaw("Horizontal") != 0 )
            {
                float moveH = Input.GetAxis("Horizontal");
                Vector2 movement = new Vector2(moveH, 0);
                rb.velocity = movement.normalized * ms;
            }

            else
            {
                if(rb.velocity.x > 0.1f || rb.velocity.x < -0.1f)
                {
                    rb.velocity *= decelerationRate;
                }
                
            }
            //ms -= 0.01f;
            //rb.velocity = new Vector2(ms * h, 0);
            //Vector2 fMS = new Vector2(ms * h, rb.position.y);
            //rb.AddForce(fMS);
        }
    }

}
