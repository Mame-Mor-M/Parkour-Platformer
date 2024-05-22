using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPlayerStats : MonoBehaviour
{
    [SerializeField] PlayerStats playerHealth;
    [SerializeField] TMP_Text hpTxt;
    [SerializeField] Image hpImg;

    private void Start()
    {
        playerHealth.health = 3;
        hpTxt.text = playerHealth.health.ToString();
        hpImg.sprite = playerHealth.healthSprite;
    }
    private void Update()
    {
        if(playerHealth.health <= 0)
        {
            playerHealth.health = 3;
            hpTxt.text = playerHealth.health.ToString();
        }
    }
}
