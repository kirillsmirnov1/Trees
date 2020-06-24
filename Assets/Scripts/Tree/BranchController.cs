using Assets.Scripts.Tree;
using System.Collections;
using UnityEngine;

public class BranchController : MonoBehaviour, ITreeElementController
{
    public GameObject branch;

    public TreeController TreeController { get; private set; }

    public int subBranches = 0;
    private bool finishedGrowing = false;

    public int generation = 0;

    private void Start()
    {
        TreeController.CheckNewBranch(transform.position);

        transform.localScale = Vector3.zero;

        float maxScale = TreeController.subBranchScaleModificator;
        float growthSpeed = TreeController.branchGrowthSpeed;
        float growthDelay = generation < 3 ? 0f : Random.Range(0f, TreeController.maxGrowthDelay);

        StartCoroutine(Grow(maxScale, growthSpeed, growthDelay));
    }

    public void SetTreeReference(TreeController treeReference)
    {
        TreeController = treeReference;
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
