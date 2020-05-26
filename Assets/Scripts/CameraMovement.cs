using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float turnSpeed;
    public Transform tree;

    // Camera position
    private Vector3 offset;
    private bool movingCamera;

    private Vector3 lastMousePos;
    private Vector3 currentMousePos;
    private Vector3 swipeInput;

    private int screenWidth;

    private void Start()
    {
        offset = new Vector3(tree.position.x, tree.position.y, tree.position.z - 5);
        screenWidth = Screen.width;
    }

    void Update()
    {
        HandleSwipeInput();

        float horizontalInput = Input.GetAxis("Horizontal") + swipeInput.x;

        offset = Quaternion.AngleAxis(horizontalInput * turnSpeed, Vector3.up) * offset;

        transform.position = tree.position + offset;
        transform.LookAt(tree.position);
    }

    private void HandleSwipeInput()
    {
        HandleMouseSwipeInput();
        HandleScreenTouchScreenSwipeInput();
    }

    private void HandleScreenTouchScreenSwipeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                movingCamera = true;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                movingCamera = false;
                swipeInput = Vector3.zero;
            }
            else 
            {
                // If touch is neither began nor finished, it's in progress
                swipeInput = touch.deltaPosition / screenWidth * 10;
            }
        }
    }

    private void HandleMouseSwipeInput()
    {
        // Swipe began
        if (Input.GetButtonDown("Fire1"))
        {
            movingCamera = true;
            lastMousePos = Input.mousePosition;
        }

        // Swipe finished
        if (Input.GetButtonUp("Fire1"))
        {
            movingCamera = false;
            swipeInput = Vector3.zero;
        }

        // Swipe in progress
        if (Input.GetButton("Fire1"))
        {
            currentMousePos = Input.mousePosition;
        }

        if (movingCamera)
        {
            swipeInput = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;
        }
    }
}
