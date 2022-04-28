using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{
    [SerializeField] GameObject inventory_gameobject;
    private bool showInventory;
    public Animator ui_animator { get; private set;}

    void Awake()
    {
        ui_animator = inventory_gameobject.GetComponent<Animator>();
    }
    

    void Update()
    {
        if (showInventory)
        {
            ui_animator.SetBool("show_inventory", true);
        }
    }

    public void ReceiveShowInventoryInput(bool _showInventory)
    {
        showInventory = _showInventory;
    }
}