using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeShop : PassiveObjects
{
    //private ShopItemButton shopItemButton;

    private void Start()
    {
        //shopItemButton = FindObjectOfType<ShopItemButton>();
        ShopItem.isShopFree = true;

    }
}
