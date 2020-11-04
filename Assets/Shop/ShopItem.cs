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
    private Inventory inventory;
    private ShopManager shopManager;

    private bool canBuyItem = true;

    private void Start()
    {
        nameObject = shopItemButton.name;
        textName.text = shopItemButton.itemTextName;
        textPrice.text = shopItemButton.itemPrice.ToString();
        imageObject.sprite = shopItemButton.itemImageObject;

        inventory = FindObjectOfType<Inventory>();
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void BuyItem()
    {
        if (inventory.goldPlayer >= shopItemButton.itemPrice && canBuyItem)
        {
            canBuyItem = false;
            inventory.goldPlayer -= shopItemButton.itemPrice;
            // On enlève le button du shop
            gameObject.SetActive(false);
            // On ajoute l'item à l'inventaire
            inventory.itemInventory.Add(gameObject);
            // On rajouter un boutton nothing
            Instantiate(shopManager.nothing, shopManager.sellButtonsParents);
            Debug.Log("A acheté l'item, donc celui-ci disparait du shop");
        }
        else
        {
            Debug.Log("N'a pas les sous pour acheter l'item");
        }
    }
}
