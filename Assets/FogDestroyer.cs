using UnityEngine;

public class FogDestroyer : MonoBehaviour
{
    private void Awake()
    {
        foreach(var fog in FindObjectsOfType<Fog>())
        {
            fog.AddDestroyer(GetComponent<Collider>());
        }
    }
}
