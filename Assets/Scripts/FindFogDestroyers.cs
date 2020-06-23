using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFogDestroyers : MonoBehaviour
{
    private const string FogDestroyerTag = "FogDestroyer";

    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        var triggers = GameObject.FindGameObjectsWithTag(FogDestroyerTag);

        for(int i = 0; i < triggers.Length; ++i)
        {
            ps.trigger.SetCollider(i, triggers[i].GetComponent<Collider>());
        }
    }
}
