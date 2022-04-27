using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform container;
    private Transform template;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void Start()
    {
        container = transform.Find("Container");
        template = container.Find("Template");
    }

    public void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float cellSize = 24f;
        foreach (Item item in inventory.itemList)
        {
            RectTransform rect_transform = Instantiate(template, container).GetComponent<RectTransform>();
            rect_transform.gameObject.SetActive(true);
            Debug.Log(rect_transform.name);
            rect_transform.anchoredPosition = new Vector2(x * cellSize, y * cellSize);
            Image image = rect_transform.Find("Icon").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
        }
    }
}
