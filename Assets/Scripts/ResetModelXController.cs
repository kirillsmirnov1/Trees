using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetModelXController : MonoBehaviour, ITouchable
{
    private void OnObjectTouch()
    {
        GameController.RestartLevel();
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
