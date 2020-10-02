using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    //Déclaration des variables permettant de créer une liste qui ressource les objets de l'inventaire
    private int currentContentIndex=0;
    public List<ItemScriptableObject> content = new List<ItemScriptableObject>();
    private int lengthList;

    private UsingItems usingItems;

    private void Start()
    {
        usingItems = FindObjectOfType<UsingItems>();
        lengthList = content.Count;
    }

    public void ConsumePotion()
    {
        usingItems.AddHp();
    }


    //ItemScriptableObject currentItem = content[currentContentIndex];
    //UsingItems.AddHp();
    //    content.Remove(currentItem);
}
