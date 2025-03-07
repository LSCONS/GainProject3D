using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private PlayerInput playerInput;

    private void OnValidate()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }


    private void LateUpdate()
    {
        
    }


    private void CameraRotate()
    {

    }
}
