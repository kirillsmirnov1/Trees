using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Camera attached to player")]
    public Camera playerCamera;
    
    [Tooltip("Camera rotation speed")]
    public float rotationSpeed = 200f;
    
    private PlayerInputHandler inputHandler;
    private float cameraVerticalAngle;

    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();    
    }

    // Update is called once per frame
    void Update()
    {
        HandleCharacterMovement();
    }

    private void HandleCharacterMovement()
    {
        // Horizontal character rotation
        transform.Rotate(new Vector3(0f, inputHandler.GetLookInputsHorizontal() * rotationSpeed, 0f), Space.Self);

        cameraVerticalAngle += inputHandler.GetLookInputsVertical() * rotationSpeed;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }
}
