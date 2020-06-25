using UnityEngine;

public class FogController : MonoBehaviour
{
    private const string FogDestroyerTag = "FogDestroyer";
    private int numberOfColliders = 0;
    private ParticleSystem.TriggerModule triggerModule;

    void Start()
    {
        triggerModule = GetComponent<ParticleSystem>().trigger;
        FindFogDestroyers();
    }

    private void FindFogDestroyers()
    {
        var triggers = GameObject.FindGameObjectsWithTag(FogDestroyerTag);
        numberOfColliders = triggers.Length;

        for (int i = 0; i < numberOfColliders; ++i)
        {
            triggerModule.SetCollider(i, triggers[i].GetComponent<Collider>());
        }
    }

    public void AddDestroyer(Collider collider) => 
        triggerModule.SetCollider(numberOfColliders++, collider);
}
