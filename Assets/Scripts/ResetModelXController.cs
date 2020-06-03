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

    private void OnMouseDown()
    {
        gameController.Reset();
    }

    public void OnTouchDown()
    {
        OnMouseDown();
    }
}
