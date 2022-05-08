using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{
    [SerializeField] GameObject inventory_gameobject;
    [SerializeField] private UI_Inventory uiInventory;
    private bool showInventory;
    public Animator ui_animator { get; private set;}

    private bool getRightItem;
    private bool getLeftItem;
    private bool selectItem;
    private bool hideInventory;

    void Awake()
    {
        ui_animator = inventory_gameobject.GetComponent<Animator>();
    }


    void Update()
    {
        /*
         * add animation to inventory to be able to show/hide and get right/left item
         */
        
        if (showInventory)
        {
            ui_animator.SetBool("show_inventory", true);
        }
        if (hideInventory)
        {
            ui_animator.SetBool("show_inventory", false);
        }
    }

    public void ReceiveShowInventoryInput(bool _showInventory)
    {
        showInventory = _showInventory;
    }

    public void ReceiveGetRightLeftItems(bool _rightItem, bool _leftItem)
    {
        getRightItem = _rightItem;
        getLeftItem = _leftItem;
    }

    public void ReceiveHideInventory(bool _hide_inventory)
    {
        hideInventory = _hide_inventory;
    }
    
}