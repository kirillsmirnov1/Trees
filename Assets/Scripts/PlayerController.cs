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
    
    [Tooltip("Maximum movement speed")]
    public float maxSpeedOnGround = 10f;
    [Tooltip("Low value changes player movement speed slowly, high value faster")]
    public float movementSharpnessOnGround = 15;

    public Vector3 characterVelocity;

    private PlayerInputHandler inputHandler;
    private CharacterController characterController;
    private float cameraVerticalAngle;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
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

        // Vertical character rotation
        cameraVerticalAngle += inputHandler.GetLookInputsVertical() * rotationSpeed;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);

        // Character movement
        Vector3 worldspaceMoveInput = transform.TransformVector(inputHandler.GetMoveInput());
        Vector3 targetVelocity = worldspaceMoveInput * maxSpeedOnGround;
        characterVelocity = Vector3.Lerp(characterVelocity, targetVelocity, movementSharpnessOnGround * Time.deltaTime);

        characterController.Move(characterVelocity * Time.deltaTime);
    }
}
