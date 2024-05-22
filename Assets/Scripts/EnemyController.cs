using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    

    public GameObject basicPrefab;
    public Transform startLocation;

    void Start()
    {
        

        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) {
            Instantiate(basicPrefab, new Vector2(startLocation.transform.position.x, startLocation.transform.position.y), Quaternion.identity);
        }
        
    }
}


