using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Création d'un gameObject qui gère le shop
    public GameObject shopManagerUI;

    public GameObject consommablePanel;
    public GameObject weaponsPanel;

    public GameObject[] shopItems;
    private int cost;

    public Text goldPlayerShopView;

    // Pour récupérer le ShopPnj et le Player
    private ShopPNJ shopPnj;
    private Player player;

    // Pour récupérer l'inventory
    private Inventory inventory;

    // Distance à partir de laquelle le joueur peut activer le shop
    // Pour l'instant en SerializeField pour modifier la distance voulue
    [SerializeField] private float distanceToShop = 5f;
    private bool isInRange;

    private void Start()
    {
        shopPnj = FindObjectOfType<ShopPNJ>();
        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<Inventory>();

        // Par défaut, on active le Consommable Panel
        consommablePanel.SetActive(true);
        weaponsPanel.SetActive(false);

        Test();
    }

    private void Update()
    {
        CanShop();
        OnPlayerShop();
        GoldViewOnShop();
    }

    private void CanShop()
    {
        if (Vector3.Distance(shopPnj.transform.position, player.transform.position) <= distanceToShop) isInRange = true;
        else isInRange = false;
    }

    // Méthode pour activer le shop
    private void OnPlayerShop()
    {
        if (shopPnj.isPlayerShopping == true && isInRange)
        {
            shopManagerUI.SetActive(true);
            player.currentEtat = Player.EtatJoueur.shopping;
        }
    }

    // Méthode pour désactiver le shop
    // Déclarer en public car on l'associe au button
    public void PlayerLeaveShop()
    {
        shopManagerUI.SetActive(false);
        shopPnj.isPlayerShopping = false;
        player.currentEtat = Player.EtatJoueur.normal;
    }

    public void ConsommablePanel()
    {
        consommablePanel.SetActive(true);
        weaponsPanel.SetActive(false);
    }

    public void WeaponsPanel()
    {
        weaponsPanel.SetActive(true);
        consommablePanel.SetActive(false);
    }

    public void GoldViewOnShop()
    {
        goldPlayerShopView.text = inventory.goldPlayer.ToString();
    }

    private void Test()
    {
        foreach (GameObject item in shopItems)
        {
            print("Le nom de l'item" + item.name);
            Text textContent = gameObject.GetComponent<Text>();
            print(textContent);
           

        //string tempParse = item.
        //cost = int.Parse(tempParse);
        //print(cost + " de type " + cost.GetType());
        }
    }
}
