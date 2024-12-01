using UnityEngine;

public class plug : MonoBehaviour
{
    [SerializeField] GameObject door;
    private DoubleSlidingDoorController door_script;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        door_script = door.GetComponent<DoubleSlidingDoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rope")
        {
            Rigidbody ropeRB = other.transform.GetComponent<Rigidbody>();
            ropeRB.isKinematic = true;
            ropeRB.useGravity = false;
            other.transform.position = transform.position;
            door_script.openDoor = true;




        }
    }
}
