using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public float minY { get; private set; } = 0;
    public float maxY { get; private set; } = 0;

    public int subBranchesLimit = 4;
    public float scaleModificator = 0.5f;

    private CameraController cameraController;

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
        bool centerMoved = false;

        if(y < minY)
        {
            minY = y;
            centerMoved = true;
        } 
        else if(y > maxY)
        {
            maxY = y;
            centerMoved = true;
        }

        if (centerMoved)
        {
            cameraController.ShowWholeTree((maxY + minY)/2f, maxY - minY);
        }
    }
}
