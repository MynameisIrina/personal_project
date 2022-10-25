using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject aim_camera;
    [SerializeField] GameObject pivot_point;
    [SerializeField] private GameObject aim_pivot_point;
    private Vector2 look_input;
    private float look_hor;
    private float prev_hor;
    private float prev_vert;
    private float look_vert;
    private float distance_to_target = 7f;
    private RaycastHit hit;
    private bool aimInput;

    private void Start()
    {
        aim_camera.SetActive(false);

    }
    

    private void Update()
    {

        if (aimInput)
        {
            camera.SetActive(false);
            aim_camera.SetActive(true);
        }
        else
        {
            camera.SetActive(true);
            aim_camera.SetActive(false);
        }
    }


    // Update is called once per frame
    private void LateUpdate()
    {
        // get an input from the player
        look_hor += look_input.x;
        look_vert -= look_input.y;
        
        // clamp the view to create a realistic field of view
        look_vert = Mathf.Clamp(look_vert, -15f, 40f);
        Vector3 target = new Vector3(look_vert, look_hor);
        camera.transform.eulerAngles = target;

        // let camera follow the player
        camera.transform.position = pivot_point.transform.position - (camera.transform.forward * distance_to_target);

        /*
         * Distinguish between two cameras: Aiming and Original.
         *  Aiming: Bring camera closer to turn on the aim.
         *  Original: orbiting around a pivot point approach
         */
        


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

            
            Debug.DrawLine(camera.transform.position, pivot_point.transform.position);

    }

    public GameObject getCamera()
    {
        if (_inputManager.aim_input)
        {
            return aim_camera;
        }
        return camera;
    }
    

    public void ReceiveAimInput(bool aim_input)
    {
        aimInput = aim_input;
    }
    
    public void receiveInputLook(Vector2 _look)
    {
        look_input = _look;
    }
    
    
}
