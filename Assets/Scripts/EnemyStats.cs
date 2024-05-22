using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();
    Enemy basicEnemy = new Enemy();
    Enemy medEnemy = new Enemy();
    Enemy speedEnemy = new Enemy();

    private string type;
    private float health;
    private float damage;
    private float speed;

    private Rigidbody2D rb;

    public PlayerHealth player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        basicEnemy = new Enemy("Normal", 50f, 5f, 5f);
        medEnemy = new Enemy("Strong", 60f, 10f, 10f);
        speedEnemy = new Enemy("Fast", 30f, 3f, 15f);

        if (this.gameObject.tag == "Normal")
        {
            enemyList.Add(basicEnemy);
            type = basicEnemy.getType();
            health = basicEnemy.getHealth();
            damage = basicEnemy.getDamage();
            speed = basicEnemy.getSpeed();
        }

         
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-1f*speed, 0.0f);

        Debug.Log("LIST SIZE: " + enemyList.Count);
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Throwable")
        {
            Destroy(other.gameObject);
            if(this.tag != "Laser")
            {
                Destroy(this.gameObject);
            }
            
        }
        if (other.tag == "Player") // Allow enemy only to collide with player but not other objects
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;
            player.health -= 1;
            
        }
        else
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        if (other.name == "LeftBorder") // Destroy enemies that go off screen
        {
            Destroy(this.gameObject);
        }
    }
}

public class Enemy
{
    private string type;
    private float health;
    private float damage;
    private float speed;


    public Enemy()
    {
        this.type = "";
        this.health = 0f;
        this.damage = 0f;
        this.speed = 0f;
    }
    public Enemy(string type, float health, float damage, float speed)
    {
        this.type = type;
        this.health = health;
        this.damage = damage;
        this.speed = speed;
    }

    public Enemy Duplicate()
    {
        return new Enemy(type, health, damage, speed);
    }

    public string getType()
    {
        return type;
    }

    public float getHealth()
    {
        return health;
    }

    public float getDamage()
    {
        return damage;
    }

    public float getSpeed()
    {
        return speed;
    }

}
