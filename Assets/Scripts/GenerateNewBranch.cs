using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewBranch : MonoBehaviour
{
    public GameObject branch;
    private TreeController treeController;

    private int subBranches = 0;

    public float rotationMax = 70;

    private void Start()
    {
        treeController = GameObject.Find("Tree").GetComponent<TreeController>();
        treeController.CheckNewBranchHeight(transform.position.y);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && subBranches < treeController.subBranchesLimit)
        {
            GenerateBranch();
        }
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

            GameObject subBranch = Instantiate(branch, head.position, rotation);
            subBranch.transform.parent = transform;
            subBranch.transform.localScale = treeController.scaleModificator * Vector3.one;
        }
    }
}
