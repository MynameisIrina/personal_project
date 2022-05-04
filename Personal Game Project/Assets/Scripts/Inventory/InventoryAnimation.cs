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

    void Awake()
    {
        ui_animator = inventory_gameobject.GetComponent<Animator>();
    }


    void Update()
    {
        if (showInventory)
        {
            ui_animator.SetBool("show_inventory", true);
            if (uiInventory.getInventory().itemList.Count > 1)
            {
                if (getRightItem)
                {
                    //uiInventory.getBorder().transform.position += new Vector3(18, 0, 0);
                }
                else if (getLeftItem)
                {
                    //uiInventory.getBorder().transform.position -= new Vector3(18, 0, 0);
                }
            }
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
}