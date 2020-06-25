using UnityEngine;

public class Fog : MonoBehaviour
{
    private int numberOfColliders = 0;

    public void AddDestroyer(Collider collider) =>
        GetComponent<ParticleSystem>().trigger.SetCollider(numberOfColliders++, collider);
}
