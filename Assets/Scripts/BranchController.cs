using UnityEngine;

public class BranchController : MonoBehaviour
{
    public GameObject branch;
    private TreeController treeController;

    private int subBranches = 0;
    private float showDebugRays = 10f;

    public float rotationMax = 70;

    private void Start()
    {
        treeController = GameObject.Find("Tree").GetComponent<TreeController>();
        treeController.CheckNewBranchHeight(transform.position.y);
    }

    private void OnCollisionStay(Collision collision)
    {
        // FIXME want to check if branch intersects with smth rn, but that doesn't work as expected
        if(collision.gameObject.transform != transform.parent)
        {
            Debug.Log("Non parent collision");
        }
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space) && subBranches < treeController.subBranchesPerBranchLimit)
        //{
        //    GenerateBranch();
        //}
    }

    private void GenerateBranch() // TODO pass generating impulse from root to limit recursion
    {
        subBranches++;

        if (Random.Range(0, 2) == 0)
        {
            Transform head = transform.Find("Head");

            Quaternion rotation = Quaternion.Euler(
                transform.rotation.eulerAngles
                + Vector3.right * Random.Range(-rotationMax, rotationMax) // TODO [-1 or 1] * Random(30, 70)
                + Vector3.forward * Random.Range(-rotationMax, rotationMax)
                );

            if (NoIntersections(head.position, rotation * Vector3.up))
            {
                GameObject subBranch = Instantiate(branch, head.position, rotation);
                subBranch.transform.parent = transform;
                subBranch.transform.localScale = treeController.subBranchScaleModificator * Vector3.one;
            }
        }
    }

    private bool NoIntersections(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        
        float maxDistance = (transform.Find("Head").position - transform.Find("Tail").position)
            .magnitude / treeController.subBranchScaleModificator;

        bool rayHitSmth = Physics.Raycast(ray, maxDistance);

        Debug.DrawRay(origin, direction * maxDistance, 
            rayHitSmth ? Color.red : Color.green, 
            showDebugRays, false);

        return !rayHitSmth;
    }
}
