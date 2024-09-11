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
    [SerializeField] private GunController gun_controller;
    [SerializeField] private ArrowController arrow_controller;

    //[SerializeField] private Arrow arrow_controller;
    private Vector2 move_value;
    private Vector2 look_input; 
    public bool aim_input { get; private set; }
    private bool fire_input;
    private bool put_away;
    private bool climb;
    private bool pick_up;
    private bool sword_attack;
    private bool jump_input;
    
    // UI
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private InventoryAnimation _inventoryAnimation;
    [SerializeField] private SelectItem SelectItemScript;
    
    
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
            // Quaternion rotation = Quaternion.Euler(1, camera_controller.GetCamera().transform.eulerAngles.y, 1);
            // player_controller.GetComponent<Transform>().transform.rotation = Quaternion.RotateTowards(player_controller.GetComponent<Transform>().rotation, rotation, 2f * Time.deltaTime);
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

        };
        _actionController.Player.Aim.canceled += ctx => aim_input = false;

         _actionController.Player.Fire.performed += ctx => 
         {
             fire_input = ctx.ReadValueAsButton();
             if (currentItem == Item.ItemType.Arrow && player_controller.GetMoveValues() == Vector2.zero)
             {
                 arrow_controller.Shoot();
             }
             else if (currentItem == Item.ItemType.Gun && player_controller.GetMoveValues() == Vector2.zero)
             {
                 gun_controller.Shoot();
             }

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
        
        _actionController.Player.Putaway.performed += ctx => put_away = ctx.ReadValueAsButton();
        _actionController.Player.Putaway.canceled += ctx => put_away = false;
        
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
            selectItem = ctx.ReadValueAsButton();
            if (selectItem && player_controller.GetInventory().itemList.Count > 0)
            {
                Debug.Log("Capacity = " + player_controller.GetInventory().itemList.Count);
                currentItem = player_controller.GetInventory().itemList[counter-1].itemType; // track current item
                player_controller.SetCurrentItem(currentItem);
            }
        };
        
        _actionController.UI.SelectItem.canceled += ctx => selectItem = false;
        _actionController.UI.HideInventory.performed += ctx => hideInventory = ctx.ReadValueAsButton();
        _actionController.UI.HideInventory.canceled += ctx => hideInventory = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        player_controller.ReceiveInputMovement(move_value);
        camera_controller.ReceiveInputLook(look_input);
        player_controller.ReceiveInputLook2(look_input);
        player_controller.ReceiveJumpInput(jump_input);
        player_controller.ReceiveAimInput(aim_input);
        camera_controller.ReceiveAimInput(aim_input);
        player_controller.ReceivePickUpInput(pick_up);
        player_controller.ReceivePutAwayInput(put_away);
        player_controller.ReceiveSwordAttackInput(sword_attack);
        player_controller.ReceiveFireInput(fire_input);
        arrow_controller.ReceiveAimInput(aim_input);
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
