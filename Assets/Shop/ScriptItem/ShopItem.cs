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

    private void Start()
    {
        nameObject = shopItemButton.name;
        textName.text = shopItemButton.itemTextName;
        textPrice.text = shopItemButton.itemPrice.ToString();
        imageObject.sprite = shopItemButton.itemImageObject;

        inventory = FindObjectOfType<Inventory>();
        shopManager = FindObjectOfType<ShopManager>();

        button = gameObject.GetComponent<Button>();
    }

    private void Update()
    {
        SetButtonIteractible();
    }

    private void SetButtonIteractible()
    {
        if (gameObject.CompareTag("NothingButton"))
        {
            button.interactable = false;
        }
        else
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
        
    }

    public void BuyItem()
    {
        if (inventory.goldPlayer >= shopItemButton.itemPrice)
        {
            // Décompte les sous du joueur
            inventory.goldPlayer -= shopItemButton.itemPrice;
            // On enlève l'item du shop. Pour ce faire, on change les caractéristiques du bouton, et on le désactive
            // Pour l'instant, on change juste le texte. Quand on détaillera le shop, il faudra reprendre ce point
            GetComponent<ShopItem>().textName.GetComponent<Text>().text = "Nothing";
            // Désactivation et non intéractabilité possible du bouton
            GetComponent<ShopItem>().enabled = false;
            button.interactable = false;
            // On ajoute l'item à l'inventaire
            inventory.itemInventory.Add(gameObject);
        }
    }
}
