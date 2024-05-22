using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : MonoBehaviour
{
    [SerializeField] float shurikenLifespan;
    void Update()
    {
        shurikenLifespan -= 1 * Time.deltaTime;

        if(shurikenLifespan <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
