using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> itemList { get; private set;}

    public Inventory()
    {
        itemList = new List<Item>();
        // AddItem(new Item{itemType = Item.ItemType.Arrow});
        // AddItem(new Item{itemType = Item.ItemType.Sword});
        // AddItem(new Item{itemType = Item.ItemType.Potion});
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
}
