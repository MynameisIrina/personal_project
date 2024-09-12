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
    public bool IsInventoryActive;
    private bool getRightItem;
    private bool getLeftItem;
    private bool selectItem;
    private bool hideInventory;

    void Awake()
    {
        IsInventoryActive = false;
        ui_animator = inventory_gameobject.GetComponent<Animator>();
    }


    void Update()
    {
        
        if (showInventory)
        {
            IsInventoryActive = true;
            ui_animator.SetBool("show_inventory", true);
        }
        if (hideInventory)
        {
            IsInventoryActive = false;
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