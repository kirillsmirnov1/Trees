﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour, ITouchable
{
    private TreeController treeController;

    private void Start()
    {
        treeController = transform.parent.gameObject.GetComponent<TreeController>();
    }

    private void OnMouseDown()
    {
        treeController.GenerateNewBranches();
    }

    public void OnTouchDown()
    {
        OnMouseDown();
    }
}
