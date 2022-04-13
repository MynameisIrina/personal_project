using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody rb;
    private bool fire_input;


    private bool arrowInBow = true;
    private bool arrow_stopped = false;
    private Vector3 aim_world_position;
    [SerializeField] private GameObject crosshair;
    private Vector3 spawn_arrow;
    [SerializeField] private LayerMask player_layermask;
    

    private void FixedUpdate()
    {
        if (arrowInBow)
        {
            return;
        }
        
        // if an arrow isn't in a bow and didn't hit anything...
        if (!arrow_stopped)
        {
            Vector3 direction = (aim_world_position - transform.position).normalized;
            rb.velocity = transform.forward * 200f;
            rb.MoveRotation(Quaternion.LookRotation(direction, transform.up));

        }
        // if an arrow hit sth, it shouldn't move anymore...
        else
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.angularVelocity = Vector3.zero;
        }

    }


    private void DefineCrossHair()
    {
        // define the target from the UI element
        Vector2 screen_center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        
        Ray ray = Camera.main.ScreenPointToRay(screen_center);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999f, ~player_layermask))
        {
            aim_world_position = (hit.point);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        arrow_stopped = true;
    }

    public void SetRb()
    {
        rb = gameObject.AddComponent<Rigidbody>();
    }
    
    public void ReceiveFireInput(bool _fire)
    {
        fire_input = _fire;
        if (fire_input)
        {
            arrowInBow = false;
            transform.parent = null;
            DefineCrossHair();
        }
    }
    

    
    
}
