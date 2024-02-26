using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour
{
    [SerializeField] LevelLoader loader;

    public void RunButton()
    {
        loader.LoadFightScene();
    }
}
