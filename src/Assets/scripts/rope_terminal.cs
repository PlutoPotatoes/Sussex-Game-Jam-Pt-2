using Unity.VisualScripting;
using UnityEngine;

public class rope_terminal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Rope")
        {
            other.transform.position = transform.position;
            freezePosition(other.attachedRigidbody);
        } 
    }
    private void freezePosition(Rigidbody body)
    {
        body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX;
        body.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
        body.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
    }

    private void freezeRope(RopeGrab rope)
    {
        GameObject root = rope.getRoot();
    }
}
