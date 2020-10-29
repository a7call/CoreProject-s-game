using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string nameObject;
    public Text textName;
    public Text textPrice;
    public Image imageObject;

    public ShopItemButton shopItemButton;

    private void Start()
    {
        nameObject = shopItemButton.name;
        textName.text = shopItemButton.itemTextName;
        textPrice.text = shopItemButton.itemTextPrice;
        imageObject.sprite = shopItemButton.itemImageObject;

    }
}
