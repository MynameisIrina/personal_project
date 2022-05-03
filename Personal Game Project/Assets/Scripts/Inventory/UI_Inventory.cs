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
    private Transform border;
    private RectTransform rect_transform_boarder;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void Start()
    {
        container = transform.Find("Container");
        template = container.Find("Template");
        border = container.Find("Border");
    }

    public void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float cellSize = 24f;
        foreach (Item item in inventory.itemList)
        {
            if (inventory.itemList.Count == 1)
            {
                rect_transform_boarder = Instantiate(border, container).GetComponent<RectTransform>();
                rect_transform_boarder.gameObject.SetActive(true);
            }
            RectTransform rect_transform = Instantiate(template, container).GetComponent<RectTransform>();
            rect_transform.gameObject.SetActive(true);
            rect_transform.anchoredPosition = new Vector2(x * cellSize, y * cellSize);
            Image image = rect_transform.Find("Icon").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
        }
    }

    public GameObject getBorder()
    {
        return rect_transform_boarder.gameObject;
    }

    public Inventory getInventory()
    {
        return inventory;
    }
}
