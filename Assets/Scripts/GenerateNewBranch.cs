using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewBranch : MonoBehaviour
{
    public GameObject branch;
    private TreeController treeController;

    private int subBranches = 0;
    private int subBranchesLimit = 4;

    public float rotationMax = 70;

    private void Start()
    {
        treeController = GameObject.Find("Tree").GetComponent<TreeController>();
        treeController.CheckNewBranchHeight(transform.position.y);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && subBranches < subBranchesLimit)
        {
            GenerateBranch();
        }
    }

    private void GenerateBranch() // TODO pass generating impulse from root to limit recursion
    {
        subBranches++;

        if (Random.Range(0, 1) == 0)
        {
            Transform head = transform.Find("Head");

            Quaternion rotation = Quaternion.Euler(
                transform.rotation.eulerAngles
                + Vector3.right * Random.Range(-rotationMax, rotationMax) // TODO [-1 or 1] * Random(30, 70)
                + Vector3.forward * Random.Range(-rotationMax, rotationMax)
                );

            Instantiate(branch, head.position, rotation)
                .transform.parent = transform;
        }
    }
}
