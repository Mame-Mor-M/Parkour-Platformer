using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2 : MonoBehaviour
{
    public float speed = 2;
    private float timeBtwSpawns;
    public float spawnRate;
    public GameObject[] obsticleTemplate;
    public GameObject[] spawnPoints;
    public Rigidbody2D rb;

    void Start()
    {
        timeBtwSpawns = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            int randomObstacle = Random.Range(0, obsticleTemplate.Length-1); //spawn
            int randomLength = Random.Range(spawnPoints.Length-1, 0); //spawnpoint

            Vector2 position = new Vector2(spawnPoints[randomLength].transform.position.x, transform.position.y);
            
            Instantiate(obsticleTemplate[randomObstacle], position, Quaternion.identity); //sprite
            
            timeBtwSpawns = spawnRate;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
