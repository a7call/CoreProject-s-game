using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPNJ : MonoBehaviour
{
    private Player player;

    public bool isPlayerShopping = false;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (isPlayerShopping==true) player.currentEtat = Player.EtatJoueur.shopping;
    }

    void OnMouseDown()
    {
        isPlayerShopping = true;
    }

}
