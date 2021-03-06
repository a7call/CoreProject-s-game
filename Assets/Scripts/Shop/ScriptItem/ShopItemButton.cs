﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ShopItemButton")]
public class ShopItemButton : ScriptableObject
{
    public string itemName;
    public string itemTextName;
    public int itemHealth;
    public int itemArmor;
    public float itemPrice;
    public Sprite itemImageObject;
}
