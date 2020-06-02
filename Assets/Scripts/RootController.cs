﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    private TreeController treeController;

    private void Start()
    {
        treeController = transform.parent.gameObject.GetComponent<TreeController>();
    }

    void Update()
    {
    #if (UNITY_IOS || UNITY_ANDROID)
        CatchTouch();
    #endif
    }

    private void CatchTouch()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    hit.transform.gameObject.SendMessage("OnMouseDown");
                }
            }
        }
    }

    private void OnMouseDown()
    {
        treeController.GenerateNewBranches();
    }
}
