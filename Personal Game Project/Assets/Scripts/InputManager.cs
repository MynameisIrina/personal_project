using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerActionController _actionController;
    [SerializeField] private PlayerController player_controller;
    [SerializeField] private CameraController camera_controller;
    [SerializeField] private Arrow arrow_controller;
    private Vector2 move_value;
    private bool jump_input;
    private Vector2 look_input;
    public bool aim_input { get; private set; }
    private bool fire_input;
    // Start is called before the first frame update
    void Awake()
    {
        _actionController = new PlayerActionController();
        _actionController.Player.Move.performed += ctx =>
        {
            move_value = ctx.ReadValue<Vector2>();
            Quaternion rotation = Quaternion.Euler(1, camera_controller.getCamera().transform.eulerAngles.y, 1);
            player_controller.GetComponent<Rigidbody>().MoveRotation(rotation);

        };
        _actionController.Player.Move.canceled += ctx => move_value = Vector2.zero;
        
       _actionController.Player.Look.performed += ctx =>
       {
           look_input = ctx.ReadValue<Vector2>();
       };
       
       _actionController.Player.Look.canceled += ctx =>
       {
           look_input = Vector2.zero;
           
       };
       
       _actionController.Player.Jump.performed += ctx => jump_input = ctx.ReadValueAsButton();
        _actionController.Player.Jump.canceled += ctx => jump_input = false;
        
        _actionController.Player.Aim.performed += ctx => aim_input = ctx.ReadValueAsButton();
        _actionController.Player.Aim.canceled += ctx => aim_input = false;

        _actionController.Player.Fire.performed += ctx =>
        {
            fire_input = ctx.ReadValueAsButton();
            arrow_controller.SetRb();
            arrow_controller.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            arrow_controller.GetComponent<Rigidbody>().useGravity = false;

        };
        _actionController.Player.Fire.canceled += ctx => fire_input = false;

        


    }

    // Update is called once per frame
    void Update()
    {
        player_controller.ReceiveInputMovement(move_value);
        camera_controller.receiveInputLook(look_input);
        player_controller.ReceiveInputJump(jump_input);
        player_controller.ReceiveAimInput(aim_input);
        arrow_controller.ReceiveFireInput(fire_input);
    }
    
    
    private void OnEnable()
    {
        _actionController.Enable();
    }

    private void OnDisable()
    {
        _actionController.Disable();
    }
    
}
