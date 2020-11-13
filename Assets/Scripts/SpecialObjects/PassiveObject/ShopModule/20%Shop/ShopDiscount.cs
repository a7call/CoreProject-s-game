using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDiscount : PassiveObjects
{
    //private ShopItemButton shopItemButton;

    private void Start()
    {
        //shopItemButton = FindObjectOfType<ShopItemButton>();
        ShopItem.isShopDiscount = true;
    }
}
