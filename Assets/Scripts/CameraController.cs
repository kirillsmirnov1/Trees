using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnSpeed;
    public GameObject tree;
    public Transform lookAtMePoint;

    private bool movingCamera;

    private Vector3 lastMousePos;
    private Vector3 currentMousePos;
    private Vector3 swipeInput;

    Bounds bounds;
    private float lastBoundsHeight;

    private int screenWidth;

    private void Start()
    {
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
    }

    private void MoveCameraToShowWholeTree()
    {
        if (lastBoundsHeight > 0)
        {
            float offsetK = bounds.size.y / lastBoundsHeight;
            Debug.Log("offsetK: " + offsetK);
            transform.localPosition *= offsetK;
        }

        lookAtMePoint.position = Vector3.up * bounds.center.y;

        Debug.Log("Bounds.center.y: " + bounds.center.y);
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

    // FIXME why camera encapsulates tree?
    private void EncapsulateTree()
    {
        if (bounds != null)
        {
            lastBoundsHeight = bounds.size.y;
        }

        bounds = new Bounds(tree.transform.position, Vector3.zero); 
        
        Debug.Log("Encapsulating!");
        
        // FIXME that is one fine overkill
        foreach (Renderer r in tree.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(r.bounds);
        }
    }
}
