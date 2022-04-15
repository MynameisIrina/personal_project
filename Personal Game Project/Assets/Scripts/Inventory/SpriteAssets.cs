using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAssets : MonoBehaviour
{
    public static SpriteAssets Instance { get; private set; }
    public Sprite arrow;
    public Sprite sword;
    public Sprite potion;


    private void Awake()
    {
        Instance = this;
    }

   

}
