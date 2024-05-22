using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : MonoBehaviour
{
    public float speed = 2;
    private float timeBtwSpawns;
    public float spawnRate;
    public GameObject[] obsticleTemplate;

    public GameObject[] spawnPoints;

    void Start()
    {
        timeBtwSpawns = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            int randomObstacle = Random.Range(0, obsticleTemplate.Length);
            int randomHeight = Random.Range(0,spawnPoints.Length);

            Vector2 position = new Vector2(transform.position.x, spawnPoints[randomHeight].transform.position.y);
            
            Instantiate(obsticleTemplate[randomObstacle], position, Quaternion.identity);
            
            timeBtwSpawns = spawnRate;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
