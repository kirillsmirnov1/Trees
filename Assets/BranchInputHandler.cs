using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchInputHandler : MonoBehaviour
{
    private TreeController _treeController;
    private TreeController TreeController 
    { 
        get 
        { 
            if(_treeController == null)
            {
                _treeController = GameObject.Find("Tree").GetComponent<TreeController>();
            }
            return _treeController;
        } 
    }

    private void OnMouseDown()
    {
        TreeController.GenerateNewBranches();
    }
}
