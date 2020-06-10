using System.Collections;
using UnityEngine;

public class BranchController : MonoBehaviour
{
    public GameObject branch;

    public TreeController treeController { get; private set; }

    public int subBranches = 0;
    private bool finishedGrowing = false;

    public int generation = 0;

    private void Start()
    {
        
        // treeController = GameObject.Find("Tree").GetComponent<TreeController>();
        treeController.CheckNewBranch(transform.position);

        transform.localScale = Vector3.zero;

        float maxScale = treeController.subBranchScaleModificator;
        float growthSpeed = treeController.branchGrowthSpeed;
        float growthDelay = generation < 3 ? 0f : Random.Range(0f, treeController.maxGrowthDelay);

        StartCoroutine(Grow(maxScale, growthSpeed, growthDelay));
    }

    public void SetTreeReference(TreeController treeReference)
    {
        treeController = treeReference;
    }

    private IEnumerator Grow(float maxScaleMod, float growthSpeed, float growthDelay)
    {
        Vector3 maxScale = maxScaleMod * Vector3.one;

        yield return new WaitForSeconds(growthDelay);

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
