using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnstuckButton : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnstuckInput();
    }

    void UnstuckInput()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
   

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Borders")
        {
            Restart();
        }
    }
}
