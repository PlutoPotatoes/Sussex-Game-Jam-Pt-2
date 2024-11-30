using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject player_controller;
    [SerializeField] Camera main_cam;
    [SerializeField] LayerMask interact_layers;
    [SerializeField] float humanScale;
    [SerializeField] float mechScale;
    InputAction interactAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        int grab_range = 7;
        RaycastHit hit_info;
        if (Physics.Raycast(main_cam.transform.position, main_cam.transform.forward, out hit_info, grab_range, interact_layers))
        {
            print(hit_info.ToString());
            if(hit_info.collider.tag == "Console")
            {
                exitMech();
            }
        }
    }

    private void exitMech()
    {
        //spawn mech
        // move player forward
        player_controller.transform.localScale = new Vector3(humanScale, humanScale, humanScale);
    }
    
    private void enterMech()
    {
        //go to mech head.transform
        player_controller.transform.localScale = new Vector3(mechScale, mechScale, mechScale);

    }
}
