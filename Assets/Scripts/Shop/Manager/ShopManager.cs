//using UnityEngine;
//using UnityEngine.UI;
//using Wanderer.Utils;

//public class ShopManager : MonoBehaviour
//{
//    // Création d'un gameObject qui gère le shop
//    public GameObject shopManagerUI;

//    // Déclaration de tous les objets potentiellement présents dans le shop
//    //[HideInInspector]
//    public GameObject nothing;
//    //[HideInInspector]
//    public GameObject halfHp;
//    //[HideInInspector]
//    public GameObject fullHp;
//    //[HideInInspector]
//    public GameObject halfArmor;
//    //[HideInInspector]
//    public GameObject fullArmor;

//    public Transform sellButtonsParents;

//    // Variables qui permettent de gérer le tableau du shop
//    private bool[,] tableau = new bool[3,3];
//    private int rowConsommable = 2;
//    private float initConsommablesChance = 50f;
//    private float consommablesChance;
//    private float weaponsChance = 50f;
//    private int count = 0;

//    // Variable de type Text UI qui correspond à l'argent du joueur
//    public Text goldPlayerShopView;

//    // Variable pour récuperer les données du joueur est du ShopPNJ
//    private Player player;
//    private ShopPNJ shopPnj;

//    private bool isPlayerShopping = false;

//    // Pour récupérer l'inventory
//    private Inventory inventory;

//    // Distance à partir de laquelle le joueur peut activer le shop
//    // Pour l'instant en SerializeField pour modifier la distance voulue
//    [SerializeField] private float distanceToShop = 5f;
//    private bool isInRange;
//    public bool OpenShop = false;

//    private void Start()
//    {
//        player = FindObjectOfType<Player>();
//        shopPnj = FindObjectOfType<ShopPNJ>();
//        inventory = FindObjectOfType<Inventory>();

//        CompleteTable();
//    }

//    private void Update()
//    {
//        CanShop();

//        switch (player.currentEtat)
//        {
//            default:
//                break;

//            case Player.EtatJoueur.shopping:
//                GoldViewOnShop();
//                break;
//        }
//    }

//    // Méthode qui permet au joueur d'ouvrir le shop si il se trouve dans la range
//    private void CanShop()
//    {
//        if (Vector3.Distance(shopPnj.shopPnjTransform.position, player.transform.position) <= distanceToShop) isInRange = true;
//        else isInRange = false;

//        if (isInRange == true)
//        {
//            if (OpenShop)
//            {
//                isPlayerShopping = true;
//                shopManagerUI.SetActive(true);
//                player.currentEtat = Player.EtatJoueur.shopping;
//            }
//        }
//    }

//    // Méthode pour désactiver le shop
//    // Déclarer en public car on l'instancie à l'aide d'un OnClick sur le button (elle n'est pas appellée dans le Update)
//    public void PlayerLeaveShop()
//    {
//        shopManagerUI.SetActive(false);
//        isPlayerShopping = false;
//        player.currentEtat = Player.EtatJoueur.normal;
//    }

//    // Permet de montrer dans le shop l'argent que possède le joueur
//    public void GoldViewOnShop()
//    {
//        goldPlayerShopView.text = inventory.goldPlayer.ToString();
//    }

//    private void DeleteElement()
//    {
//        // Supprime les éléments contenus par défaut
//        for (int i = 0; i < sellButtonsParents.childCount; i++)
//        {
//            Destroy(sellButtonsParents.GetChild(i).gameObject);
//        }
//    }

//    // Méthode qui permet de générer le shop de manière aléatoire
//    private void CompleteTable()
//    {
//        DeleteElement();

//        // Génère les consommables
//        // De plus si par malchance le shop est vide, on s'assure d'au moins donner le premier consommable
//        // C'est ce que à quoi sert la variable count
//        for (int row = 0; row < rowConsommable ; row++)
//        {
//            for (int column = 0; column < tableau.GetLength(1); column++)
//            {
//                tableau[row, column] = Utils.RandomBool(consommablesChance);

//                if (tableau[row, column] == false)
//                {
//                    Instantiate(nothing, sellButtonsParents);
//                    consommablesChance = 1.25f * initConsommablesChance;
//                    count++;
//                }
//                else
//                {
//                    if(row==0 && column == 0)
//                    {
//                        Instantiate(halfHp, sellButtonsParents);
//                    }
//                    else if(row == 0 && column == 1)
//                    {
//                        Instantiate(halfHp, sellButtonsParents);
//                    }
//                    else if (row == 0 && column == 2)
//                    {
//                        Instantiate(fullHp, sellButtonsParents);
//                    }
//                    else if (row == 1 && column == 0)
//                    {
//                        Instantiate(halfArmor, sellButtonsParents);
//                    }
//                    else if (row == 1 && column == 1)
//                    {
//                        Instantiate(halfArmor, sellButtonsParents);
//                    }
//                    else if (row == 1 && column == 2)
//                    {
//                        Instantiate(fullArmor, sellButtonsParents);
//                    }

//                    consommablesChance = initConsommablesChance;
//                }

//            }

//            consommablesChance = initConsommablesChance;
  
//        }

//        if (count == (rowConsommable * tableau.GetLength(1)))
//        {
//            count = 0;
//            CompleteTable();
//        }

//        // A moduler selon la rareté des armes
//        for (int row = rowConsommable ; row < tableau.GetLength(0); row++)
//        {
//            for (int column = 0; column < tableau.GetLength(1); column++)
//            {
//                tableau[row, column] = Utils.RandomBool(weaponsChance);
//            }
//        }
//    }

//    // // MIS EN COMMENTAIRE CAR ON NE L'UTILISE PAS POUR L'INSTANT
//    // // PERMET DE GENERER UN TABLEAU DE FALSE
//    //private void GenerateFalseTable()
//    //{
//    //    for (int row = 0; row < tableau.GetLength(0); row++)
//    //    {
//    //        //print("Ligne " + (row+1) + " du tableau.");
//    //        for (int column = 0; column < tableau.GetLength(1); column++)
//    //        {
//    //            tableau[row, column] = false ;
//    //            //print(tableau[row, column] + ":");
//    //        }
//    //    }
//    //}

//}
