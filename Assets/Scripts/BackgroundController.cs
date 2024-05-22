using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float paralaxEffect;

    void Start()
    {
        startPos = transform.position.x;
    }

    void FixedUpdate()
    {
        float distance = cam.transform.position.x * paralaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
