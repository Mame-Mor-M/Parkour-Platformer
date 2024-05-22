using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    //Transform Variables
    [SerializeField] Transform gate;
    [SerializeField] Transform gateBottom;
    [SerializeField] Transform closePoint;

    //Velocity Variables
    [SerializeField] float closeSpeed;

    //Gate Properties
    [SerializeField] bool upwardGate;
    private bool startClosing;
   
    void Start()
    {
        //gate = GetComponent<Transform>();
    }

    void Update()
    {
        if (gateBottom.transform.position.y > closePoint.position.y && !upwardGate && startClosing == true) // Gate is lowered until fully closed
        {
            gate.Translate(Vector2.down * closeSpeed * Time.deltaTime); // Lowers gate at the close speed rate
        }

        else if (gateBottom.transform.position.y < closePoint.position.y && upwardGate && startClosing == true) // Gate is lowered until fully closed
        {
            gate.Translate(Vector2.up * closeSpeed * Time.deltaTime); // Lowers gate at the close speed rate
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Player"))
        {
            startClosing = true;
        }
    }

    
}
