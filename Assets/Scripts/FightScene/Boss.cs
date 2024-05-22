using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float speed;
    public bool MoveUp;
    public Animator anim;

 [SerializeField] GameObject endScreen;
    [SerializeField] GameObject blocker;

    public int sceneBuildIndex;
    public Image bar;
    public int bossHealth = 200;
    public float fillAmount = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        bar.fillAmount = 1;
        MoveUp = true;
        UnityEngine.Time.timeScale = 1f;
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("Throwable") || trig.gameObject.CompareTag("Player"))
        {
            bossHealth--;
            bar.fillAmount -= 0.005f ;
            anim.SetBool("Hit", true);

            if(bossHealth <= bossHealth*0) 
            {              
                StartCoroutine("endGame");
            }
        }
    }
    
    void FixedUpdate()
    {
        anim.SetBool("Hit", false);
    }
    
    public void LoadMainMenu()
    {
      SceneManager.LoadScene("Main Menu");
    }

    public void LoadEndScene()
    {
      SceneManager.LoadScene("EndScene");
    }

    public void RestartGame()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator endGame()
    {
        endScreen.SetActive(true);
        blocker.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("EndScene");
    }
}
