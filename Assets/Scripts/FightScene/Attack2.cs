using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour
{
    public float speed = 6;
   public Animator anim;

    private Shake shake;

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            shake.CamShake();
            anim.SetBool("Explode", true);
    }
    if (collision.CompareTag("Wall"))
    {
        Destroy(gameObject);
    }
}
}