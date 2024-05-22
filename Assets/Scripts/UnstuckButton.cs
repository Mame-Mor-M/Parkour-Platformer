using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnstuckButton : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float propulsion;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
