using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
   public float speed = 6;
   public Animator anim;
   public AudioSource hurtSound;

    private Shake shake;

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            shake.CamShake();
            hurtSound.Play();
    }
    if (collision.CompareTag("Wall"))
    {
        Destroy(gameObject);
    }
}
}
