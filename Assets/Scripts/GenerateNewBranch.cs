using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewBranch : MonoBehaviour
{
    public GameObject branch;
    private CameraController cameraController;

    private int subBranches = 0;
    private int subBranchesLimit = 1;

    private void Start()
    {
        cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && subBranches < subBranchesLimit)
        {
            GenerateBranch();
        }
    }

    private void GenerateBranch()
    {
        subBranches++;
        
        Instantiate(branch, transform.position + Vector3.up, transform.rotation)
            .transform.parent = transform.parent;

        cameraController.ShowWholeTree();
    }
}
