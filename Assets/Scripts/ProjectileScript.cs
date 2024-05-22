using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public Rigidbody2D shuriken;
    public GameObject throwPos;
    public float shurikenVelocity;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K))
        { // Throw shuriken on right click
            Rigidbody2D shot = Instantiate(shuriken, new Vector3(throwPos.transform.position.x, throwPos.transform.position.y, throwPos.transform.position.z), Quaternion.identity); // Create bullet object
            shot.velocity = transform.right * shurikenVelocity; // Launch shuriken from front of player at certain speed
            Debug.Log(shot.velocity);

        }

    }
}
