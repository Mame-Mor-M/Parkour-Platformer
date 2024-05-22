using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 inputDirection = new Vector3(0, 0, 0);

        int edgePanSize = 20;

        if(Input.mousePosition.x < edgePanSize)
        {
            inputDirection.x = -1f;
        }
        if(Input.mousePosition.y < edgePanSize)
        {
            inputDirection.z = -1f;
        }
        if(Input.mousePosition.x < Screen.width - edgePanSize)
        {
            inputDirection.x = +1f;
        }
        if (Input.mousePosition.y < Screen.height - edgePanSize)
        {
            inputDirection.z = +1f;
        }

        Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;

        float moveSpeed = 50f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
