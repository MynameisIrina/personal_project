using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private GameObject bow;
    private GameObject[] weapons;
    private Item.ItemType typeOfWeapon;
    

    // Start is called before the first frame update
    void Start()
    {
        bow = GameObject.Find("BowWeapon");
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
    }



    // Update is called once per frame
    void Update()
    {
        /*
         * decide what weapon is near you
         * Approach: find all objects of type "Weapon". Calculate the distance between the player
         * and all of the weapons. If distance if small enough, check what type of weapon it is in
         * order to add it to inventory.
         */
        
        if (playerController.pick_up)
        {
            foreach (GameObject weapon in weapons)
            {
                float distanceFromPlayerToWeapon =
                    Vector3.Distance(gameObject.transform.position, weapon.transform.position);
                if (distanceFromPlayerToWeapon < 4f)
                {
                    if (weapon.name.Contains("Bow"))
                    {
                       typeOfWeapon = Item.ItemType.Arrow;
                    }
                    else if (weapon.name.Contains("Sword"))
                    {
                        typeOfWeapon = Item.ItemType.Sword;
                    }
                    
                    playerController.GetInventory().AddItem(new Item {itemType = typeOfWeapon});
                    uiInventory.RefreshInventoryItems();
                    Destroy(weapon);
                    gameObject.GetComponent<PickUpWeapon>().enabled = false;
                }
            }

        }
    }
}
    

