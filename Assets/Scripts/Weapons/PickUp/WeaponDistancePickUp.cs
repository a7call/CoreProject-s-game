using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDistancePickUp : MonoBehaviour
{
    private PlayerAttack player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = FindObjectOfType<PlayerAttack>();
            player.projectil = gameObject;
            Destroy(gameObject);
        } 
    }
}
