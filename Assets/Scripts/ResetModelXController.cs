using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetModelXController : MonoBehaviour, ITouchable
{
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnObjectTouch()
    {
        gameController.Reset();
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
