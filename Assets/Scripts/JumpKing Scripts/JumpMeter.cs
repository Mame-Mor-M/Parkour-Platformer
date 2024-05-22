using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpMeter : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] JKmovement jk;
    //[SerializeField] ImprovedJumpTest ij;
    [SerializeField] Slider jumpMeter;

    private void Start()
    {
        jumpMeter.maxValue = jk.chargeDuration;
        //jumpMeter.maxValue = ij.chargeDuration;
    }

    private void Update()
    {
        jumpMeter.value = jk.chargeCounter;
        //jumpMeter.value = ij.chargeCounter;
    }
}
