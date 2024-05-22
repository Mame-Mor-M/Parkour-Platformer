using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trap : MonoBehaviour
{
    [SerializeField] PlayerStats playerHealth;
    [SerializeField] TMP_Text hpTxt;
    [SerializeField] GameObject player;
    //[SerializeField] CheckPoints cp;

    private void Start()
    {
        playerHealth.health = 3;
        hpTxt.text = playerHealth.health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if(other.collider.CompareTag("Player"))
        {
            /*playerHealth.health -= 1;
            hpTxt.text = playerHealth.health.ToString();*/
            StartCoroutine("TakeDamage");

            //player.transform.position = checkPoints[cp.index].transform.position;
            //Teleport player back to the last checkpoint.
            //If player hp = 0, teleport player back to the beginning.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            /*playerHealth.health -= 1;
            hpTxt.text = playerHealth.health.ToString();*/
            StartCoroutine("TakeDamage");

            
            //player.transform.position = checkPoints[cp.index].transform.position;
            //Teleport player back to the last checkpoint.
            //If player hp = 0, teleport player back to the beginning.
        }
    }

    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.6f);
        playerHealth.health -= 1;
        hpTxt.text = playerHealth.health.ToString();
    }
}
