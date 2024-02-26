using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //public Animator transition;
    public float transitionTime = 1f;
    private string scene1 = "MainScene";
    private string scene2 = "FightScene";

    public int sceneBuildIndex;

    public void LoadFightScene()
    {
        SceneManager.LoadScene(scene1);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
