using System.Collections.Generic;

public class Inventory
{
    public List<Item> itemList { get; private set;}

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
}
