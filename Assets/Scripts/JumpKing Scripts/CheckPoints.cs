using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    //public Transform[] checkPoints;
    public int index;

    private void Start()
    {
        index = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            index++;
            GetComponent<Collider>().enabled = false;
        }
    }
}
