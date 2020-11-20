using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    // L'argent que possède le joueur
    public float goldPlayer;
    // Les clés que possèdent le joueur
    public int numberOfKeys;
    public int numberOfHearts;
    private int amountToAdd = 20;

    public List<GameObject> itemInventory = new List<GameObject>();

    // A priori, ne sert à rien ici
    // Lié aux modules du PowerUp
    // [HideInInspector] public static bool isPowerUp = false;

    // A priopri, ne sert à rien. Mais permet de récupérer une vie dans l'inventaire
    // public GameObject hearth;

    private void Start()
    {
        goldPlayer = 0;
    }

    private void Update()
    {
        AddGold(amountToAdd);
        //AddObject();
    }

    // Fonctionner de test pour ajouter des golds [A RETIRER PAR LA SUITE]
    private void AddGold(int _amountToAdd)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            goldPlayer += _amountToAdd;
        }
    }

    // A priori, ne sert à rien
    //private void AddObject()
    //{
    //    if (isPowerUp == true)
    //    {
    //        itemInventory.Insert(0, hearth);
    //        isPowerUp = false;
    //    }
    //}


}
