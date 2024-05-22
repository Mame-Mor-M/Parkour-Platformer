using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    //public Animator transition;
    public float transitionTime;
    private string scene1 = "MainScene";
    private string scene2 = "FightScene";
    [SerializeField] GameObject creditMenu;
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject endScreen;
    private bool cutscene;
    public int sceneBuildIndex;


    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (cutscene == true && Input.GetKeyDown(KeyCode.Space))
        {
            sceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
    public void LoadFightScene()
    {
        SceneManager.LoadScene(scene1);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(this.name == "Level3")
            {
                endScreen.SetActive(true);
                cutscene = true;
                StartCoroutine("NextLevel");
            }
            else
            {
                cutscene = false;
                StartCoroutine("levelTransition");
                
            }
            
        }
    }


    public void LoadLevelOne()
    {
        startScreen.SetActive(true);
        cutscene = true;
        StartCoroutine("StartGame");
        
    }

    public void LoadCredits()
    {
        if(creditMenu.activeSelf)
        {
            creditMenu.SetActive(false);
        }
        else
        {
            creditMenu.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene1);
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }
    IEnumerator levelTransition()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
