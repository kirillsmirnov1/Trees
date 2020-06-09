using System.Collections;
using UnityEngine;

public class BranchController : MonoBehaviour
{
    public GameObject branch;

    private TreeController treeController;

    public int subBranches = 0;
    private bool finishedGrowing = false;

    private void Start()
    {
        
        treeController = GameObject.Find("Tree").GetComponent<TreeController>();
        treeController.CheckNewBranch(transform.position.y);

        transform.localScale = Vector3.zero;

        float maxScale = treeController.subBranchScaleModificator;
        float growthSpeed = treeController.branchGrowthSpeed;

        StartCoroutine(Grow(maxScale, growthSpeed));
    }

    private IEnumerator Grow(float maxScaleMod, float growthSpeed)
    {
        Vector3 maxScale = maxScaleMod * Vector3.one;

        while(maxScale.x > transform.localScale.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, maxScale, growthSpeed * Time.deltaTime);

            yield return new WaitForSeconds(0.01f);
        }

        finishedGrowing = true;
    }

    public bool CanGrowSubBranches()
    {
        return finishedGrowing;
    }
}
