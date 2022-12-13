using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Camera camera;
    [SerializeField] private Camera gun_camera;
    [SerializeField] private Camera arrow_camera;
    [SerializeField] GameObject pivot_point;
    private PlayerController playerController;
    private Vector2 look_input;
    private float look_hor;
    private float look_vert;
    private float distance_to_target = 7f;
    private RaycastHit hit;
    private bool aimInput;
    private SelectItem selectItem;

    private void Start()
    {
        gun_camera.enabled = false;
        arrow_camera.enabled = false;
        playerController = player.GetComponent<PlayerController>();
        selectItem = gameObject.GetComponent<SelectItem>();

    }
    

    private void Update()
    {
        // check if player is aiming and he picked an item from his inventory
        if (aimInput && selectItem.ifItemisSelected())
        {
            
            if (playerController.getCurrentItem() == Item.ItemType.Arrow)
            {
                camera.enabled = false;
                gun_camera.enabled = false;
                arrow_camera.enabled = true;
            }
            else if (playerController.getCurrentItem() == Item.ItemType.Gun)
            {
                camera.enabled = false;
                gun_camera.enabled = true;
                arrow_camera.enabled = false;
            }

        }
        else
        {
            arrow_camera.enabled = false;
            gun_camera.enabled = false;
            camera.enabled = true;
        }
    }
    
    private void LateUpdate()
    {
        moveCamera();
    }

    private void moveCamera()
    {
        // Original: orbiting around a pivot point approach
        if (camera.enabled)
        {
            // get an input from the player
            look_hor += look_input.x;
            look_vert -= look_input.y;
        
            // clamp the view to create a realistic field of view
            look_vert = Mathf.Clamp(look_vert, -15f, 40f);
            Vector3 target = new Vector3(look_vert, look_hor);
            camera.transform.eulerAngles = target;

            // let camera follow the player
            camera.transform.position = pivot_point.transform.position - 
                                        (camera.transform.forward * distance_to_target);
            


            // if there is a collision between camera and other object - move the camera closer to the player
            if (Physics.Linecast(camera.transform.position, pivot_point.transform.position, out hit))
            {
                if (hit.collider.tag != "Player")
                {
                    camera.transform.position = pivot_point.transform.position -
                                                (camera.transform.forward *
                                                 Vector3.Distance(pivot_point.transform.position, hit.point) * 0.5f);
                }

            }
        }
    }

    public Camera getCamera()
    {
        if (inputManager.aim_input)
        {
            if (playerController.getCurrentItem() == Item.ItemType.Arrow)
            {
                return arrow_camera;

            }
            else if (playerController.getCurrentItem() == Item.ItemType.Gun)
            {
                return gun_camera;

            }
        }
        return camera;
    }
    

    public void ReceiveAimInput(bool aim_input)
    {
        aimInput = aim_input;
    }
    
    
    public void ReceiveInputLook(Vector2 _look)
    {
        look_input = _look;
    }
    
    
}
