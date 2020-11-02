using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Création d'un gameObject qui gère le shop
    public GameObject shopManagerUI;

    public GameObject shopPanel;

    [SerializeField] private List<ShopItemButton> shopList = new List<ShopItemButton>();
    public ShopItemButton healthPotion;
    private int shopListIndex = 0;
    private int numberOfConsommable ;

    private bool[,] tableau = new bool[3,3];
    private int rowConsommable = 2;
    private float initConsommablesChance = 50f;
    private float consommablesChance;
    private float weaponsChance = 50f;

    private int cost;

    public Text goldPlayerShopView;

    private Player player;
    private ShopPnj shopPnj;

    private bool isPlayerShopping = false;

    // Pour récupérer l'inventory
    private Inventory inventory;

    // Distance à partir de laquelle le joueur peut activer le shop
    // Pour l'instant en SerializeField pour modifier la distance voulue
    [SerializeField] private float distanceToShop = 5f;
    private bool isInRange;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        shopPnj = FindObjectOfType<ShopPnj>();
        inventory = FindObjectOfType<Inventory>();
        
        //GenerateRandomNumberOfConsommable();

        // On génére un tableau de false en début de jeu
        GenerateFalseTable();
        // On remplit un tableau aléatoirement
        CompleteTable();
    }

    private void Update()
    {
        CanShop();
        OnPlayerShop();


        switch (player.currentEtat)
        {
            default:
                break;

            case Player.EtatJoueur.shopping:
                //Definir tout ce qu'on veut faire dedans
                GenerateRandomObjectInShop();
                GoldViewOnShop();
                break;
        }
    }

    private void CanShop()
    {
        if (Vector3.Distance(shopPnj.shopPnjTransform.position, player.transform.position) <= distanceToShop) isInRange = true;
        else isInRange = false;

        if (isInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isPlayerShopping = true;
            }
        }
    }

    // Méthode pour activer le shop
    private void OnPlayerShop()
    {
        if (isPlayerShopping == true)
        {
            shopManagerUI.SetActive(true);
            player.currentEtat = Player.EtatJoueur.shopping;
            shopPanel.SetActive(true);
        }
    }

    // Méthode pour désactiver le shop
    // Déclarer en public car on l'associe au button
    public void PlayerLeaveShop()
    {
        shopManagerUI.SetActive(false);
        isPlayerShopping = false;
        player.currentEtat = Player.EtatJoueur.normal;
    }
    public void GoldViewOnShop()
    {
        goldPlayerShopView.text = inventory.goldPlayer.ToString();
    }
    
    //private int GenerateRandomNumberOfConsommable()
    //{
    //        numberOfConsommable = (int)Random.Range(1f, 3f);
    //        print(numberOfConsommable);
    //        return numberOfConsommable;
    //}

    private void GenerateRandomObjectInShop()
    {
        if (player.currentEtat == Player.EtatJoueur.shopping && shopList.Count<numberOfConsommable)
        {
            shopList.Insert(0, healthPotion);
        }
    }

    private void GenerateFalseTable()
    {
        for (int row = 0; row < tableau.GetLength(0); row++)
        {
            //print("Ligne " + (row+1) + " du tableau.");
            for (int column = 0; column < tableau.GetLength(1); column++)
            {
                tableau[row, column] = false ;
                //print(tableau[row, column] + ":");
            }
        }
    }

    private void CompleteTable()
    {
        for (int row = 0; row < rowConsommable ; row++)
        {
            print("Ligne " + (row + 1) + " du tableau.");
            for (int column = 0; column < tableau.GetLength(1); column++)
            {
                tableau[row, column] = RandomBoolean.RandomBool(consommablesChance);

                if (tableau[row, column] == false)
                {
                    consommablesChance = 1.5f * initConsommablesChance;
                }
                else
                {
                    consommablesChance = initConsommablesChance;
                }

                Debug.LogWarning(tableau[row, column] + ":");
            }

            consommablesChance = initConsommablesChance;
  
        }

        // A moduler selon la rareté des armes
        for (int row = rowConsommable ; row < tableau.GetLength(0); row++)
        {
            print("Ligne " + (row + 1) + " du tableau.");
            for (int column = 0; column < tableau.GetLength(1); column++)
            {
                tableau[row, column] = RandomBoolean.RandomBool(weaponsChance);
                Debug.Log(tableau[row, column] + ":");
            }
        }
    }

    // Fonction qui ajoute l'item de la liste à l'invitaire si il est présent dans la liste
    //private void AddItemToInventory()
    //{
    //    foreach (ShopItemButton item in shopPanel)
    //    {

    //    }
    //}

    private void RemoveItemOfShopList()
    {
        // Fonction qui retire l'item de la liste si il n'est plus présent dans le panel
    }

    //private void Test()
    //{
    //    foreach (GameObject item in shopItems)
    //    {
    //        print("Le nom de l'item" + item.name);
            
    //        Text textContent = item.GetComponent<ShopItem>().textPrice.GetComponent<Text>();
    //        print(textContent.text);

    //    //string tempParse = item.
    //    //cost = int.Parse(tempParse);
    //    //print(cost + " de type " + cost.GetType());
    //    }
    //}
}
