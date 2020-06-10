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
                _treeController = transform.parent.GetComponent<BranchController>().treeController;
            }
            return _treeController;
        } 
    }

    private void OnMouseDown()
    {
        TreeController.GenerateNewBranches();
    }
}
