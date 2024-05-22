using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth2 : MonoBehaviour
{
  public Image healthBar;
  public int health = 5;
  public int maxHealth = 5;
  public float fillAmount = 0.2f;

   void Start()
  {
    healthBar.fillAmount = 1;
    UnityEngine.Time.timeScale = 1f;
  }
  void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("Laser"))
        {
            health--;
            healthBar.fillAmount -= fillAmount;
          
          if(health == maxHealth*0)
          {
            UnityEngine.Time.timeScale = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          }
        }
    }
}
