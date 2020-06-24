using Assets.Scripts.Tree;
using UnityEngine;

public class RootController : MonoBehaviour, ITreeElementController
{
    public TreeController TreeController { get; private set;  }

    private void Start()
    {
        TreeController = transform.parent.gameObject.GetComponent<TreeController>();
    }
}
