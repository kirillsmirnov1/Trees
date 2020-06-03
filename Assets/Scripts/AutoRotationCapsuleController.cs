using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotationCapsuleController : MonoBehaviour, ITouchable
{
    private CameraController cameraController;

    void Start()
    {
        cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
    }

    private void OnObjectTouch()
    {
        cameraController.FlipRotationFlag();
    }

    void ITouchable.OnTouchDown()
    {
        OnObjectTouch();
    }

    private void OnMouseDown()
    {
        OnObjectTouch();
    }
}
