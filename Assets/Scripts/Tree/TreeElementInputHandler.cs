using Assets.Scripts.Tree;
using UnityEngine;

public class TreeElementInputHandler : MonoBehaviour, ITouchable
{
    private TreeController _treeController;
    private TreeController TreeController 
    { 
        get 
        { 
            if(_treeController == null)
            {
                _treeController = transform.parent.GetComponent<ITreeElementController>().TreeController;
            }
            return _treeController;
        } 
    }

    private void OnMouseDown()
    {
        OnObjectTouch();
    }

    private void OnObjectTouch()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            TreeController.ResetTree();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            TreeController.GrowFullTree();
        }
        else
        {
            TreeController.GenerateNewBranches();
        }
    }

    public void OnTouchDown()
    {
        OnObjectTouch();
    }
}
