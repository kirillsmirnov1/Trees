using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetModelXController : MonoBehaviour, ITouchable
{
    private void OnObjectTouch()
    {
        GameController.RestartLevel();
    }

    void ITouchable.OnClick()
    {
        OnObjectTouch();
    }

    private void OnMouseDown()
    {
        OnObjectTouch();
    }
}
