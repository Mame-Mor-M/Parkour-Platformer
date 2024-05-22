using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBoss : MonoBehaviour
{
    [SerializeField] GameObject pauseBoss;

    public void Pause()
    {
        pauseBoss.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseBoss.SetActive(false);
        Time.timeScale = 1f;
    }

}
