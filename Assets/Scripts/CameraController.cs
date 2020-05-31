using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnSpeed;
    public Transform lookAtMePoint;

    private bool movingCamera;

    private Vector3 lastMousePos;
    private Vector3 currentMousePos;
    private Vector3 swipeInput;

    private int screenWidth;

    private void Start()
    {
        screenWidth = Screen.width;

        ShowWholeTree(0, 1);
    }

    public void ShowWholeTree(float center, float treeHeight)
    {
        MoveCameraToShowWholeTree(center, treeHeight);
    }

    void Update()
    {
        HandleSwipeInput();
    }

    private void MoveCameraToShowWholeTree(float center, float treeHeight)
    {
        // FIXME still not the best way
        transform.localPosition = transform.localPosition.normalized * treeHeight * 2f;

        lookAtMePoint.position = Vector3.up * center;
    }

    private void HandleSwipeInput()
    {
        HandleMouseSwipeInput();
        HandleScreenTouchScreenSwipeInput();

        float horizontalInput = Input.GetAxis("Horizontal") + swipeInput.x;
        lookAtMePoint.rotation = Quaternion.Euler(lookAtMePoint.rotation.eulerAngles + Vector3.up * horizontalInput * turnSpeed);
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
