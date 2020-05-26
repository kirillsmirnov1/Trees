using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float turnSpeed;
    public Transform tree;

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(tree.position.x, tree.position.y, tree.position.z - 5);    
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        offset = Quaternion.AngleAxis(-1 * horizontalInput * turnSpeed, Vector3.up) * offset;
        
        transform.position = tree.position + offset;
        transform.LookAt(tree.position);
    }
}
