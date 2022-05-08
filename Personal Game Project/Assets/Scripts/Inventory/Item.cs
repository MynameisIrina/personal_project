using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Arrow,
        Sword,
        Potion

    }
    
    public ItemType itemType;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Arrow: return SpriteAssets.Instance.arrow;
            case ItemType.Sword: return SpriteAssets.Instance.sword;
            case ItemType.Potion: return SpriteAssets.Instance.potion;
            default: return null;
            
        }
    }

}
