﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnSpeed;
    public GameObject tree;

    // Camera position
    private Vector3 offset;
    private bool movingCamera;

    private Vector3 lastMousePos;
    private Vector3 currentMousePos;
    private Vector3 swipeInput;

    Bounds bounds;
    private float lastBoundsHeight;

    private int screenWidth;
    private Vector3 lookAtMe;

    private void Start()
    {
        offset = new Vector3(tree.transform.position.x, tree.transform.position.y, tree.transform.position.z - 5);
        screenWidth = Screen.width;

        ShowWholeTree();
    }

    public void ShowWholeTree()
    {
        EncapsulateTree();
        MoveCameraToShowWholeTree();
    }

    void Update()
    {
        HandleSwipeInput();

        float horizontalInput = Input.GetAxis("Horizontal") + swipeInput.x;

        offset = Quaternion.AngleAxis(horizontalInput * turnSpeed, Vector3.up) * offset;

        transform.position = tree.transform.position + offset;
        transform.LookAt(lookAtMe);
    }

    private void MoveCameraToShowWholeTree()
    {
        if (lastBoundsHeight > 0)
        {
            float offsetK = bounds.size.y / lastBoundsHeight;
            Debug.Log("offsetK: " + offsetK);
            offset *= offsetK;
        }

        offset = new Vector3(offset.x, bounds.center.y, offset.z);

        Debug.Log("Bounds.center.y: " + bounds.center.y);
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

    private void EncapsulateTree()
    {
        if (bounds != null)
        {
            lastBoundsHeight = bounds.size.y;
        }
        bounds = new Bounds(tree.transform.position, Vector3.zero);

        foreach (Renderer r in tree.GetComponentsInChildren<Renderer>())
        {
            Debug.Log("Encapsulating!");
            bounds.Encapsulate(r.bounds);
        }

        lookAtMe = bounds.center;
    }
}