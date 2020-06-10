using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.Find("Player").GetComponent<PlayerCheckpointHandler>().AddCheckPoint(gameObject);
        Debug.Log("Checkpoint activated");
    }
}
