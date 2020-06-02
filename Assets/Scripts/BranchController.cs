using UnityEngine;

public class BranchController : MonoBehaviour
{
    public GameObject branch;
    private TreeController treeController;

    public int subBranches = 0;

    private void Start()
    {
        treeController = GameObject.Find("Tree").GetComponent<TreeController>();
        treeController.CheckNewBranchHeight(transform.position.y);
    }
}
