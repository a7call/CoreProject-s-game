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

    private Button button;

    // Module de shop
    public static bool isShopFree = false;
    public static bool isShopDiscount = false; 
    private float discount;
    private float newPrice;

    // Afin de modifier les caractéristiques du joueur après son achat
    private PlayerHealth playerHealth;

    private void Start()
    {
        nameObject = shopItemButton.name;
        textName.text = shopItemButton.itemTextName;
        textPrice.text = shopItemButton.itemPrice.ToString();
        imageObject.sprite = shopItemButton.itemImageObject;

        inventory = FindObjectOfType<Inventory>();
        shopManager = FindObjectOfType<ShopManager>();

        button = gameObject.GetComponent<Button>();

        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        SetButtonIteractible();
        DiscountShop();
    }

    private void DiscountShop()
    {
        if (isShopDiscount == true)
        {
            discount = 20;
            newPrice = shopItemButton.itemPrice - (discount / 100f * shopItemButton.itemPrice);
            GetComponent<ShopItem>().textPrice.GetComponent<Text>().text = newPrice.ToString();
        }

        if (isShopFree == true)
        {
            newPrice = 0;
            GetComponent<ShopItem>().textPrice.GetComponent<Text>().text = newPrice.ToString();
        }
    }

    private void SetButtonIteractible()
    {
        if (gameObject.CompareTag("NothingButton"))
        {
            button.interactable = false;
        }

        if(isShopDiscount==false && isShopFree == false)
        {
            if (inventory.goldPlayer >= shopItemButton.itemPrice)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        else
        {
            if (inventory.goldPlayer >= newPrice)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }

    }

    public void BuyItem()
    {
        if (isShopDiscount == false && isShopFree == false)
        {
            // Décompte les sous du joueur
            inventory.goldPlayer -= shopItemButton.itemPrice;
            // On enlève l'item du shop. Pour ce faire, on change les caractéristiques du bouton, et on le désactive
            // Quand on détaillera le shop, il faudra reprendre les trois lignes ci-dessous!!
            GetComponent<ShopItem>().textName.GetComponent<Text>().text = "Nothing";
            GetComponent<ShopItem>().textPrice.GetComponent<Text>().text = "0";
            GetComponent<ShopItem>().imageObject.GetComponent<Image>().sprite = null;
            // Désactivation et non intéractabilité possible du bouton
            GetComponent<ShopItem>().enabled = false;
            button.interactable = false;
            // On ajoute l'item à l'inventaire
            inventory.itemInventory.Add(gameObject);
        }
        else if (isShopDiscount == true && isShopFree == true)
        {
            newPrice = 0f;
            inventory.goldPlayer -= newPrice;
            GetComponent<ShopItem>().textName.GetComponent<Text>().text = "Nothing";
            GetComponent<ShopItem>().textPrice.GetComponent<Text>().text = "0";
            GetComponent<ShopItem>().imageObject.GetComponent<Image>().sprite = null;
            // Désactivation et non intéractabilité possible du bouton
            GetComponent<ShopItem>().enabled = false;
            button.interactable = false;
            // On ajoute l'item à l'inventaire
            inventory.itemInventory.Add(gameObject);
        }
        else
        {
            inventory.goldPlayer -= newPrice;
            GetComponent<ShopItem>().textName.GetComponent<Text>().text = "Nothing";
            GetComponent<ShopItem>().textPrice.GetComponent<Text>().text = "0";
            GetComponent<ShopItem>().imageObject.GetComponent<Image>().sprite = null;
            // Désactivation et non intéractabilité possible du bouton
            GetComponent<ShopItem>().enabled = false;
            button.interactable = false;
            // On ajoute l'item à l'inventaire
            inventory.itemInventory.Add(gameObject);
        }

        playerHealth.AddLifePlayer(shopItemButton.itemHealth);
    }
}
