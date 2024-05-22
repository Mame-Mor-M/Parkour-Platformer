using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfCharges;

    public Image[] charges;
    public Sprite fullCharge;
    public Sprite emptyCharge;


    private EnemyStats enemy;

    private void Update()
    {
        if(health > numOfCharges)
        {
            health = numOfCharges;
        }
        for(int i = 0; i < charges.Length; i++)
        {
            if (i < health)
            {
                charges[i].sprite = fullCharge;
            }
            else
            {
                charges[i].sprite = emptyCharge;
            }
            if (i < numOfCharges)
            {
                charges[i].enabled = true;
            }
            else
            {
                charges[i].enabled = false;
            }
        }
        if (health <= 0)
        {
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
