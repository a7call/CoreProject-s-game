using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe de pick up des armes distances.
/// Lors du ramassage de l'arme l'attribut projectile du player est remplacé par le gameobject 
/// </summary>
public class WeaponDistancePickUp : MonoBehaviour
{
    private PlayerAttack player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = FindObjectOfType<PlayerAttack>();
            player.projectile = gameObject;
            Destroy(gameObject);
        } 
    }
}
