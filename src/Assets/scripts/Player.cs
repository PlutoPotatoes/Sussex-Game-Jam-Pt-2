using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject player_controller;
    [SerializeField] Camera main_cam;
    [SerializeField] LayerMask interact_layers;
    [SerializeField] float humanScale;
    [SerializeField] float mechScale;
    [SerializeField] GameObject mech;
    [SerializeField] Transform objectGrabPoint;
    InputAction interactAction;
    public bool isInMech;
    private  const float interactCooldown = 1.0f;

    private double lastInteract;
    private GameObject mechInstance;
    private RopeGrab heldRope;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInMech = true;
        lastInteract = 0;
        heldRope = null;
        player_controller.transform.localScale = new Vector3(mechScale, mechScale, mechScale);
        interactAction = InputSystem.actions.FindAction("Interact");   
    }

    // Update is called once per frame
    void Update()
    {
        input_listener();
    }

    private void input_listener()
    {
        if (interactAction.IsPressed())
        {
            interact();
        }
    }

    private void interact()
    {
        // only let user interact if it's been 1 second since last interact
        if ((Time.timeAsDouble - lastInteract) >= interactCooldown)
        {
            int grab_range = 600;
            RaycastHit hit_info;
            if (Physics.Raycast(main_cam.transform.position, main_cam.transform.forward, out hit_info, grab_range, interact_layers))
            {
                lastInteract = Time.timeAsDouble;
                if (hit_info.collider != null && heldRope == null)
                {
                    if (hit_info.collider.tag == "Console")
                    {
                        print(hit_info.collider.gameObject.ToString());
                        Vector3 spawnPoint = hit_info.collider.transform.position;
                        exitMech(spawnPoint);
                    }
                    else if (hit_info.collider.tag == "Mech")
                    {
                        enterMech();

                    }
                    else if (hit_info.collider.tag == "Rope")
                    {
                        print("attempting to grab rope " + hit_info.collider.gameObject.ToString());
                        grabRope(hit_info);
                    }

                }
                else if (hit_info.collider != null)
                {
                    heldRope.Grab(null);
                    heldRope = null;
                }
            }
        }
    }

    private void grabRope(RaycastHit hitInfo)
    {
       heldRope = hitInfo.transform.GetComponentInParent<RopeGrab>();
       print(heldRope);
       heldRope.Grab(objectGrabPoint);

    }

    private void exitMech(Vector3 spawnPoint)
    {
        //spawn mech
        // move player to spawn position on podium
        mechInstance = Instantiate(mech, player_controller.transform.position, player_controller.transform.rotation);
        player_controller.transform.position = new Vector3(0, 0, 0);
        transform.position = spawnPoint;
        player_controller.transform.localScale = new Vector3(humanScale, humanScale, humanScale);
        
        isInMech = false;
    }
    
    private void enterMech()
    {
        player_controller.transform.position = mechInstance.transform.position;
        Destroy(mechInstance);
        player_controller.transform.localScale = new Vector3(mechScale, mechScale, mechScale);
        isInMech = true;

    }
}