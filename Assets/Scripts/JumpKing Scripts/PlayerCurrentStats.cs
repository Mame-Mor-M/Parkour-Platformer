using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentStats : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] Transform startPoint;

    private void Update()
    {
        if(stats.health == 0)
        {
            gameObject.transform.position = startPoint.transform.position;
            //stats.health = 3;
        }
        
    }
}
