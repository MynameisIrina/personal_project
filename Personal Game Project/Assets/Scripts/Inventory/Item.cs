using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Gun,
        Sword,
        Potion,
        Arrow
    }
    
    public ItemType itemType;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Arrow: return SpriteAssets.Instance.arrow;
            case ItemType.Sword: return SpriteAssets.Instance.sword;
            case ItemType.Potion: return SpriteAssets.Instance.potion;
            case ItemType.Gun: return SpriteAssets.Instance.gun;

            default: return null;
            
        }
    }

}
