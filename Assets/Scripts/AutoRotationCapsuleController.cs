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

    private void OnMouseDown()
    {
        cameraController.FlipRotationFlag();
    }

    void ITouchable.OnTouchDown()
    {
        OnMouseDown();
    }
}
