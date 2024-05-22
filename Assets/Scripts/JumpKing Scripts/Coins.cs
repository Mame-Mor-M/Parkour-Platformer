using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Coins : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] ScoreSystem ss;
    //[SerializeField] float score;


    private void Start()
    {
        ss.score = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            ss.score++;
            scoreText.text = ss.score.ToString();
        }
    }
}
