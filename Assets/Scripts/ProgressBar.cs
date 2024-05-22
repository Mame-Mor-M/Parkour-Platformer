using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Slider progressBar;
    [SerializeField] Transform finishPoint, playerCurrentPoint;

    private void Start()
    {
        progressBar.minValue = 0;
        progressBar.maxValue = finishPoint.transform.position.x;
    }

    private void Update()
    {
        progressBar.value = playerCurrentPoint.transform.position.x;
    }
}

