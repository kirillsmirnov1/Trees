using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public float lowestTreeY { get; private set; } = 0;
    public float highestTreeY { get; private set; } = 0;

    public int subBranchesPerBranchLimit;
    public float subBranchScaleModificator;

    public TextMeshProUGUI branchesCounterText;
    public GameObject branch;

    private CameraController cameraController;
    private int numberOfBranches = 0;

    private List<BranchController> branches = new List<BranchController>();

    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateNewBranches();
        }
    }

    private void GenerateNewBranches()
    {
        if(branches.Count == 0)
        {
            GenerateFirstBranch();
        }
        else
        {
            Debug.Log("TODO: generate other branches");
        }
    }

    private void GenerateFirstBranch()
    {
        branches.Add(Instantiate(branch, transform).GetComponent<BranchController>());
    }

    public void CheckNewBranchHeight(float y)
    {
        numberOfBranches++;
        UpdateBranchesText();

        bool centerMoved = false;

        if(y < lowestTreeY)
        {
            lowestTreeY = y;
            centerMoved = true;
        } 
        else if(y > highestTreeY)
        {
            highestTreeY = y;
            centerMoved = true;
        }

        if (centerMoved)
        {
            cameraController.ShowWholeTree((highestTreeY + lowestTreeY)/2f, highestTreeY - lowestTreeY);
        }
    }

    private void UpdateBranchesText()
    {
        branchesCounterText.text = "Branches: " + numberOfBranches;
    }
}
