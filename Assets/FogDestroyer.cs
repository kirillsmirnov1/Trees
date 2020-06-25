using UnityEngine;

public class FogDestroyer : MonoBehaviour
{
    private void Awake()
    {
        foreach(var fog in FindObjectsOfType<FogController>())
        {
            fog.AddDestroyer(GetComponent<Collider>());
        }
    }
}
