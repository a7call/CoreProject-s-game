using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    // L'argent que possède le joueur
    public int goldPlayer;
    public int numberOfKeys;
    public int numberOfHearts;
    private int amountToAdd = 20;

    public List<GameObject> itemInventory = new List<GameObject>();

    private void Start()
    {
        goldPlayer = 0;
    }

    private void Update()
    {
        AddGold(amountToAdd);
    }

    // Fonctionner de test pour ajouter des golds [A RETIRER PAR LA SUITE]
    private void AddGold(int _amountToAdd)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            goldPlayer += _amountToAdd;
        }
    }


}
