using System.Collections;
using UnityEngine;

/// <summary>
///  Classe gérant les attaques du joueur
/// </summary>

public class PlayerCollision: Player
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        if (collision.CompareTag("Key"))
        {
            inventory.numberOfKeys++;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("HalfHeart"))
        {
            inventory.numberOfHearts += 1;
            
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("FullHeart"))
        {
            inventory.numberOfHearts += 2;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Currency"))
        {
            inventory.goldPlayer += 1;
            Destroy(collision.gameObject);
        }

    }


}

