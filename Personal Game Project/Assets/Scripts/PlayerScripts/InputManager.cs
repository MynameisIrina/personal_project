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
    [SerializeField] private ArrowInstantiate arrow_controller;
    //[SerializeField] private Arrow arrow_controller;
    private Vector2 move_value;
    private bool jump_input;
    private Vector2 look_input; 
    public bool aim_input { get; private set; }
    private bool fire_input;
    private bool put_away;
    private bool climb;
    private bool pick_up;
    private bool sword_attack;
    
    // UI
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private InventoryAnimation _inventoryAnimation;
    [SerializeField] private SelectItem SelectItemScript;

    [SerializeField] private GameObject arrow;
    private Item.ItemType currentItem;
    private bool showInventory;
    private bool getRightItem;
    private bool getLeftItem;
    private bool selectItem;
    private bool hideInventory;
    private int counter = 1;


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
        
        _actionController.Player.Aim.performed += ctx =>
        {
            aim_input = ctx.ReadValueAsButton();
            gameObject.GetComponent<ArrowInstantiate>().SpawnArrow();

        };
        _actionController.Player.Aim.canceled += ctx => aim_input = false;

         _actionController.Player.Fire.performed += ctx => 
         {
             //Debug.Log("Im here InputManager");
             arrow.GetComponent<ArrowMove>().enabled = true;
             fire_input = ctx.ReadValueAsButton();
         };
        _actionController.Player.Fire.canceled += ctx => fire_input = false;

        _actionController.Player.Pickup.performed += ctx => pick_up = ctx.ReadValueAsButton();
        _actionController.Player.Pickup.canceled += ctx => pick_up = false;
        
        _actionController.Player.Putaway.performed += ctx => put_away = ctx.ReadValueAsButton();
        _actionController.Player.Putaway.canceled += ctx => put_away = false;

        _actionController.Player.Climb.performed += ctx => climb = ctx.ReadValueAsButton();
        _actionController.Player.Climb.canceled += ctx => climb = false;

        _actionController.Player.SwordAttack.performed += ctx => sword_attack = ctx.ReadValueAsButton();
        _actionController.Player.SwordAttack.canceled += ctx => sword_attack = false;


        // UI
        
        _actionController.UI.ShowInventory.performed += ctx => showInventory = ctx.ReadValueAsButton();
        _actionController.UI.ShowInventory.canceled += ctx => showInventory = false;
        
        _actionController.Player.Putaway.performed += ctx => showInventory = ctx.ReadValueAsButton();
        _actionController.Player.Putaway.canceled += ctx => showInventory = false;
        
        _actionController.UI.GetRightItem.performed += ctx =>
        {
            getRightItem = ctx.ReadValueAsButton();
            if (counter < uiInventory.getInventory().itemList.Count)
            {
                counter++;
                uiInventory.getBorder().GetComponent<RectTransform>().localPosition += new Vector3(25, 0, 0);
            }
        };
        _actionController.UI.GetRightItem.canceled += ctx => getRightItem = false;
        _actionController.UI.GetLeftItem.performed += ctx =>
        {
            if (counter > 1)
            {
                counter--;
                uiInventory.getBorder().GetComponent<RectTransform>().localPosition -= new Vector3(25, 0, 0);
                getLeftItem = ctx.ReadValueAsButton();
            }
        };
        _actionController.UI.GetLeftItem.canceled += ctx => getLeftItem = false;
        
        _actionController.UI.SelectItem.performed += ctx =>
        {
            Debug.Log("Counter: " + counter );
            selectItem = ctx.ReadValueAsButton();
            currentItem = player_controller.GetInventory().itemList[counter-1].itemType; // track current item
        };
        _actionController.UI.SelectItem.canceled += ctx => selectItem = false;
        
        _actionController.UI.HideInventory.performed += ctx => hideInventory = ctx.ReadValueAsButton();
        _actionController.UI.HideInventory.canceled += ctx => hideInventory = false;








    }

    // Update is called once per frame
    void Update()
    {
        player_controller.ReceiveInputMovement(move_value);
        camera_controller.receiveInputLook(look_input);
        player_controller.ReceiveInputJump(jump_input);
        player_controller.ReceiveAimInput(aim_input);
        player_controller.ReceivePickUpInput(pick_up);
        player_controller.ReceivePutAwayInput(put_away);
        player_controller.ReceiveClimbInput(climb);
        player_controller.ReceiveSwordAttackInput(sword_attack);
        //arrow_controller.ReceiveFireInput(fire_input);
        _inventoryAnimation.ReceiveShowInventoryInput(showInventory);
        _inventoryAnimation.ReceiveGetRightLeftItems(getRightItem, getLeftItem);
        _inventoryAnimation.ReceiveHideInventory(hideInventory);
        SelectItemScript.ReceiveSelectItem(selectItem, currentItem);
        
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
