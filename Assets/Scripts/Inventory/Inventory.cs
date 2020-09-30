using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Déclaration des variables permettant de créer une liste qui ressource les objets de l'inventaire
    private int currentContentIndex=0;
    public List<ItemScriptableObject> content = new List<ItemScriptableObject>();
    private int lengthList;

    private void Start()
    {
        lengthList = content.Count;
    }

}
