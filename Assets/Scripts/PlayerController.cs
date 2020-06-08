using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Camera attached to player")]
    public Camera playerCamera;

    [Header("General")]
    [Tooltip("Force applied downward when in the air")]
    public float gravityDownForce = 20f;
    [Tooltip("Physic layers checked to consider the player grounded")]
    public LayerMask groundCheckLayers = -1;
    [Tooltip("Distance from the bottom of the character controller capsule to test for grounded")]
    public float groundCheckDistance = 0.05f;

    [Header("Rotation")]
    [Tooltip("Camera rotation speed")]
    public float rotationSpeed = 200f;

    [Header("Movement")]
    [Tooltip("Maximum movement speed")]
    public float maxSpeedOnGround = 10f;
    [Tooltip("Low value changes player movement speed slowly, high value faster")]
    public float movementSharpnessOnGround = 15;
    [Tooltip("Acceleration speed when in the air")]
    public float accelerationSpeedInAir = 25f;
    [Tooltip("Max horizontal movement speed when not on ground")]
    public float maxSpeedInAir = 10f;

    public Vector3 characterVelocity;

    private PlayerInputHandler inputHandler;
    private CharacterController characterController;
    private Vector3 groundNormal;
    private float cameraVerticalAngle;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<PlayerInputHandler>();    
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        HandleCharacterMovement();
    }

    private void GroundCheck()
    {
        float chosenGroundCheckDistance = characterController.skinWidth + groundCheckDistance;

        // Drop to defaults, so falling would work right
        isGrounded = false;
        groundNormal = Vector3.up;

        if (Physics.CapsuleCast(
                GetCapsuleBottomPoint(), GetCapsuleTopPoint(), characterController.radius, 
                Vector3.down, out RaycastHit hit, chosenGroundCheckDistance, 
                groundCheckLayers, QueryTriggerInteraction.Ignore))
        {
            groundNormal = hit.normal;

            // Only consider this a valid ground hit if the ground normal goes in the same direction as the character up
            // and if the slope angle is lower than the character controller's limit
            if (Vector3.Dot(hit.normal, transform.up) > 0f &&
                IsNormalUnderSlopeLimit(groundNormal))
            {
                isGrounded = true;

                if(hit.distance > characterController.skinWidth)
                {
                    characterController.Move(Vector3.down * hit.distance);
                }
            }
        }
    }

    // Returns true if the slope angle represented by the given normal is under the slope angle limit of the character controller
    bool IsNormalUnderSlopeLimit(Vector3 normal)
    {
        return Vector3.Angle(transform.up, normal) <= characterController.slopeLimit;
    }

    // Gets the center point of the bottom hemisphere of the character controller capsule    
    Vector3 GetCapsuleBottomPoint()
    {
        // FIXME that doesn't seem right
        return transform.position + (transform.up * characterController.radius);
    }

    // Gets the center point of the top hemisphere of the character controller capsule    
    Vector3 GetCapsuleTopPoint()
    {
        float atHeight = characterController.height;
        return transform.position + (transform.up * (atHeight - characterController.radius));
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

        if (isGrounded)
        {
            Vector3 targetVelocity = worldspaceMoveInput * maxSpeedOnGround;
            targetVelocity = GetDirectionReorientedOnSlope(targetVelocity.normalized, groundNormal) * targetVelocity.magnitude;

            characterVelocity = Vector3.Lerp(characterVelocity, targetVelocity, movementSharpnessOnGround * Time.deltaTime);
        }
        else // Not grounded
        {
            characterVelocity += worldspaceMoveInput * accelerationSpeedInAir * Time.deltaTime;

            // limit air speed to a maximum, but only horizontally
            Vector3 verticalVelocity = characterVelocity.y * Vector3.up;
            Vector3 horizontalVelocity = Vector3.ClampMagnitude(Vector3.ProjectOnPlane(characterVelocity, Vector3.up), maxSpeedInAir);
            characterVelocity = horizontalVelocity + verticalVelocity;

            // Apply gravity
            characterVelocity += Vector3.down * gravityDownForce * Time.deltaTime;
        }
        // Apply movement
        characterController.Move(characterVelocity * Time.deltaTime);
    }

    // Gets a reoriented direction that is tangent to a given slope
    public Vector3 GetDirectionReorientedOnSlope(Vector3 direction, Vector3 slopeNormal)
    {
        Vector3 directionRight = Vector3.Cross(direction, transform.up);
        return Vector3.Cross(slopeNormal, directionRight).normalized;
    }
}
