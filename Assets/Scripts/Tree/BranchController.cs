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
        TreeController = transform.GetComponentInParent<TreeController>();
        TreeController.CheckNewBranch(transform.position);

        transform.localScale = Vector3.zero;

        float maxScale = TreeController.subBranchScaleModificator;
        float growthSpeed = TreeController.branchGrowthSpeed;
        float growthDelay = generation < 3 ? 0f : Random.Range(0f, TreeController.maxGrowthDelay);

        StartCoroutine(Grow(maxScale, growthSpeed, growthDelay));
    }

    private IEnumerator Grow(float resultScaleMod, float growthSpeed, float growthDelay = 0f, bool destroyInTheEnd = false)
    {
        Vector3 resultScale = resultScaleMod * Vector3.one;

        yield return new WaitForSeconds(growthDelay);

        while(Mathf.Abs(resultScale.x - transform.localScale.x) > 0.001f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, resultScale, growthSpeed * Time.deltaTime);

            yield return new WaitForSeconds(0.01f);
        }

        finishedGrowing = true;

        if(destroyInTheEnd)
        {
            Destroy(gameObject);
        }
    }

    public bool CanGrowSubBranches()
    {
        return finishedGrowing;
    }

    public void DestroyBranch() 
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out BranchController branch))
                branch.DestroyBranch();
        }

        StartCoroutine(
            Grow(
                resultScaleMod: 0f, 
                growthSpeed: TreeController.branchGrowthSpeed / 2, 
                destroyInTheEnd: true
                )
            );
    }
}
