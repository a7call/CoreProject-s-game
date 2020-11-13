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

    // Déclaration de tous les objets potentiellement présents dans le shop
    public GameObject nothing;
    public GameObject halfHp;
    public GameObject oneHp;
    public Transform sellButtonsParents;

    // Variables qui permettent de gérer le tableau du shop
    private bool[,] tableau = new bool[3,3];
    private int rowConsommable = 2;
    private float initConsommablesChance = 50f;
    private float consommablesChance;
    private float weaponsChance = 50f;

    // private int cost;

    // Variable de type Text UI qui correspond à l'argent du joueur
    public Text goldPlayerShopView;

    // Variable pour récuperer les données du joueur est du ShopPNJ
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

        // On génére un tableau de false en début de jeu
        //GenerateFalseTable();
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
                GoldViewOnShop();
                break;
        }
    }

    // Méthode pour savoir si le joueur est en range d'ouvrir le shop
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
        }
    }

    // Méthode pour désactiver le shop
    // Déclarer en public car on l'instancie à l'aide d'un OnClick sur le button (de ce fait, elle n'est pas appellée dans le Update)
    public void PlayerLeaveShop()
    {
        shopManagerUI.SetActive(false);
        isPlayerShopping = false;
        player.currentEtat = Player.EtatJoueur.normal;
    }

    // Permet de montrer dans le shop l'argent que possède le jour
    public void GoldViewOnShop()
    {
        goldPlayerShopView.text = inventory.goldPlayer.ToString();
    }

    // On appelle la fonction en appuyant sur le button
    //public void BuyAnItem()
    //{
    //    Text A = gameObject.GetComponent<ShopItem>().textPrice.GetComponent<Text>();
    //    string tempParse = A;
    //    cost = int.Parse(tempParse);
    //    print(cost + " de type " + cost.GetType());


    //    if (inventory.goldPlayer >= )
    //    {

    //    }
    //}

    // Mis en commentaire car on ne l'utilise pas pour l'instant
    //private void GenerateFalseTable()
    //{
    //    for (int row = 0; row < tableau.GetLength(0); row++)
    //    {
    //        //print("Ligne " + (row+1) + " du tableau.");
    //        for (int column = 0; column < tableau.GetLength(1); column++)
    //        {
    //            tableau[row, column] = false ;
    //            //print(tableau[row, column] + ":");
    //        }
    //    }
    //}

    private void CompleteTable()
    {
        // Supprime les éléments contenus par défaut
        for (int i = 0; i < sellButtonsParents.childCount; i++)
        {
            Destroy(sellButtonsParents.GetChild(i).gameObject);
        }

        // Génère les consommables
        // Si le tableau est vide de base, on s'assure d'au moins donner le premier objet
        int count = 0;
        for (int row = 0; row < rowConsommable ; row++)
        {
            print("Ligne " + (row + 1) + " du tableau.");
            for (int column = 0; column < tableau.GetLength(1); column++)
            {
                tableau[row, column] = RandomBoolean.RandomBool(consommablesChance);

                if (tableau[row, column] == false)
                {
                    Instantiate(nothing, sellButtonsParents);
                    consommablesChance = 1.25f * initConsommablesChance;
                    count++;
                }
                else
                {
                    if(row==0 && column == 0)
                    {
                        Instantiate(halfHp, sellButtonsParents);
                        
                    }
                    else if(row == 0 && column == 1)
                    {
                        Instantiate(halfHp, sellButtonsParents);
                    }
                    else if (row == 0 && column == 2)
                    {
                        Instantiate(oneHp, sellButtonsParents);
                    }
                    else if (row == 1 && column == 0)
                    {
                        Instantiate(halfHp, sellButtonsParents);
                    }
                    else if (row == 1 && column == 1)
                    {
                        Instantiate(halfHp, sellButtonsParents);
                    }
                    else if (row == 1 && column == 2)
                    {
                        Instantiate(oneHp, sellButtonsParents);
                    }

                    consommablesChance = initConsommablesChance;
                }

                Debug.LogWarning(tableau[row, column] + ":");
            }

            consommablesChance = initConsommablesChance;
  
        }

        if (count==(rowConsommable * tableau.GetLength(1)))
        {
            print("A");
            Instantiate(halfHp, sellButtonsParents);
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
