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

    private void Start()
    {
        nameObject = shopItemButton.name;
        textName.text = shopItemButton.itemTextName;
        textPrice.text = shopItemButton.itemPrice.ToString();
        imageObject.sprite = shopItemButton.itemImageObject;

        inventory = FindObjectOfType<Inventory>();
        shopManager = FindObjectOfType<ShopManager>();
    }

    private void Update()
    {
        SetButtonIteractible();
    }

    private void SetButtonIteractible()
    {
        Button button = gameObject.GetComponent<Button>();

        if (inventory.goldPlayer >= shopItemButton.itemPrice)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void BuyItem()
    {
        if (inventory.goldPlayer >= shopItemButton.itemPrice)
        {
            inventory.goldPlayer -= shopItemButton.itemPrice;
            print("La pos du button à détruire" + gameObject.transform);
            // On enlève le button du shop
            // Voir comment récupérer la position du boutton sur lequel on clique
            Instantiate(shopManager.oneHp, gameObject.transform);
            gameObject.SetActive(false);
            // On ajoute l'item à l'inventaire
            inventory.itemInventory.Add(gameObject);
            // On rajouter un boutton nothing
            Debug.Log("A acheté l'item, donc celui-ci disparait du shop");
        }
        else
        {
            Debug.Log("N'a pas les sous pour acheter l'item");
        }
    }
}
