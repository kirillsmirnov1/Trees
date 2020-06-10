using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCheckpointHandler : MonoBehaviour
{
    private List<GameObject> checkPoints = new List<GameObject>();

    public void AddCheckPoint(GameObject checkPoint)
    {
        checkPoints.Add(checkPoint);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fog"))
        {
            HandleCheckPoints();
        }
    }

    private void HandleCheckPoints()
    {
        GameObject closestCp = null;
        float minDistance = float.MaxValue;

        foreach(GameObject checkPoint in checkPoints)
        {
            Debug.DrawLine(checkPoint.transform.position, transform.position, Color.blue, 0.01f);
            float d = Vector3.Distance(checkPoint.transform.position, transform.position);
            if (d < minDistance)
            {
                closestCp = checkPoint;
                minDistance = d;
            }
        }

        Debug.DrawLine(transform.position, closestCp.transform.position, Color.green, 0.02f);
    }
}
