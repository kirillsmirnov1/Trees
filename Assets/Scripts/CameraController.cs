using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnSpeed;
    public Transform lookAtMePoint;

    public float verticalOffset = 1.5f;
    public float verticalModifier = 2.5f; // TODO add user control: wheel+pinch

    public float autoRotateSpeed = 0.25f;

    private bool movingCamera;

    private float lastTreeCenter;
    private float lastTreeHeight;

    private Vector3 lastMousePos;
    private Vector3 currentMousePos;
    private Vector3 swipeInput;

    private int screenWidth;

    private bool isAutorotateEnabled = true;

    private void Start()
    {
        screenWidth = Screen.width;

        ShowWholeTree(0, 1);
    }

    public void ShowWholeTree(float center, float treeHeight)
    {
        lastTreeCenter = center;
        lastTreeHeight = treeHeight;

        MoveCameraToShowWholeTree();
    }

    void Update()
    {
        HandleSwipeInput();

        if(Input.GetKeyDown(KeyCode.O))
        {
            isAutorotateEnabled = !isAutorotateEnabled;
        }
    }

    public void MoveCameraToShowWholeTree()
    {
        transform.localPosition = transform.localPosition.normalized * (lastTreeHeight + verticalOffset) * verticalModifier;

        lookAtMePoint.position = Vector3.up * lastTreeCenter;
    }

    private void HandleSwipeInput()
    {
        HandleMouseSwipeInput();
        HandleScreenTouchScreenSwipeInput();

        float horizontalInput = Input.GetAxis("Horizontal") + swipeInput.x;

        lookAtMePoint.Rotate(Vector3.up * (horizontalInput + (isAutorotateEnabled ? autoRotateSpeed : 0)) * turnSpeed);
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
