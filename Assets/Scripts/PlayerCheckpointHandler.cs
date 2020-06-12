using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCheckpointHandler : MonoBehaviour
{
    private readonly List<GameObject> checkPoints = new List<GameObject>();
    private GameObject closestCheckpoint;
    private readonly float checkpointMaxDistance = 100f;

    private readonly float checkpointCheckRate = 1f;

    public void AddCheckPoint(GameObject checkPoint)
    {
        checkPoints.Add(checkPoint);
    }

    private void Start()
    {
        StartCoroutine(CheckCheckpointDistance());
    }

    private IEnumerator CheckCheckpointDistance()
    {
        if (NeedToRecheckCheckpoint())
        {
            HandleCheckPoints();
        }
        yield return new WaitForSeconds(checkpointCheckRate);
        StartCoroutine(CheckCheckpointDistance());
    }

    private bool NeedToRecheckCheckpoint()
    {
        if (closestCheckpoint == null) return true;
        return Vector3.Distance(closestCheckpoint.transform.position, transform.position) > checkpointMaxDistance;
    }

    private void HandleCheckPoints()
    {
        GameObject closestCp = null;
        float minDistance = float.MaxValue;

        foreach(GameObject checkPoint in checkPoints)
        {
            Debug.DrawLine(checkPoint.transform.position, transform.position, Color.blue, 1f);
            float d = Vector3.Distance(checkPoint.transform.position, transform.position);
            if (d < minDistance)
            {
                closestCp = checkPoint;
                minDistance = d;
            }
        }

        closestCheckpoint = closestCp;

        Debug.DrawLine(transform.position, closestCp.transform.position, Color.green, 1f);
    }
}
