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

    private void Start()
    {
        aim_camera.SetActive(false);

    }


    public void receiveInputLook(Vector2 _look)
    {
        look_input = _look;
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
        Vector3 vel = Vector3.zero;
        // camera.transform.position = Vector3.SmoothDamp(camera.transform.position,
        //     pivot_point.transform.position - (camera.transform.forward * distance_to_target), ref vel, 0.005f);
        camera.transform.position = pivot_point.transform.position - (camera.transform.forward * distance_to_target);

        /*
         * Distinguish between two cameras: Aiming and Original.
         *  Aiming: Bring camera closer to turn on the aim.
         *  Original: orbiting around a pivot point approach
         */
        
       // if (!_inputManager.aim_input)
        //{
            camera.SetActive(true);
            aim_camera.SetActive(false); 
            
            
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
            //Debug.Log(Physics.Linecast(camera.transform.position, gameObject.transform.position, out hit, player_layerMask));
            
            
            
            
        //}

    }

    public GameObject getCamera()
    {
        if (_inputManager.aim_input)
        {
            return aim_camera;
        }
        return camera;
    }

    public Vector2 getLookInput()
    {
        return look_input;
    }
    
    
}
