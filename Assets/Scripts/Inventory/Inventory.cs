using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{

    public int goldPlayer;
    private int amountToAdd = 20;

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
