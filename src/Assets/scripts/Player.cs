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
    InputAction interactAction;
    public bool isInMech;

    private GameObject mechInstance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInMech = true;
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
        int grab_range = 600;
        RaycastHit hit_info;
        if (Physics.Raycast(main_cam.transform.position, main_cam.transform.forward, out hit_info, grab_range, interact_layers))
        {
            if (hit_info.collider != null)
            {
                if (isInMech && hit_info.collider.tag == "Console")
                {
                    print(hit_info.collider.gameObject.ToString());
                    Vector3 spawnPoint = hit_info.collider.transform.position;
                    exitMech(spawnPoint);
                }else if(hit_info.collider.tag == "Mech")
                {
                    enterMech();

                }
            }
        }
    }

    private void exitMech(Vector3 spawnPoint)
    {
        mechInstance = Instantiate(mech, player_controller.transform.position, player_controller.transform.rotation);
        player_controller.transform.localScale = new Vector3(humanScale, humanScale, humanScale);
        player_controller.transform.position = new Vector3(-149.350006f, 11.3599997f, 214.309998f);

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
