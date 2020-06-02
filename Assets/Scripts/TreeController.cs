using System.Collections;
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

    private CameraController cameraController;
    private int numberOfBranches = 0;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
