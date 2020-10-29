using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Voir si on peut supprimer ce code
/// Mais pour l'instant, j'ai trouvé que ce moyen pour faire fonctionner le clique de souris lorsque l'on clique sur le marchand
/// Car il faut déclarer un collider sur le Marchand + un scrip avec la fonction OnMouseDown
/// </summary>
public class ShopPNJ : MonoBehaviour
{

    public bool isPlayerShopping = false;

    void OnMouseDown()
    {
        isPlayerShopping = true;
    }

}
