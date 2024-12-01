using UnityEngine;

public class RopeGrab : MonoBehaviour
{
    [SerializeField] GameObject root;
    // part of rope that is grabbed
    private Rigidbody objectRigidBody;
    // where rope is being dragged to
    private Transform objectGrabPoint;
    private bool held;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        held = false;
        objectRigidBody = transform.GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        print("rope grabbed");
        objectGrabPoint = objectGrabPointTransform;
        held = objectGrabPoint != null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objectGrabPoint != null)
        {
            print("moving object to grab point: " + objectGrabPoint.position);
            float lerp = 10f;
            // if object grab point exists
            // 1. go to new position
            // 2. disable gravity
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPoint.position, Time.deltaTime * lerp);
            transform.position = newPosition;
            objectRigidBody.useGravity = false;
        }
        else
        {
            // go back to normal
            objectRigidBody.useGravity = true;
        }
    }

    public bool isHeld()
    {
        return held;
    }

    public GameObject getRoot()
    {
        return root;
    }
}
