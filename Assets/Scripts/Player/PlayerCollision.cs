using System.Collections;
using System.Globalization;
using UnityEngine;

/// <summary>
///  Classe gérant les attaques du joueur
/// </summary>

public class PlayerCollision: Player
{
    [SerializeField] private int numberOfAmoInCase = 0;

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
            GetComponent<PlayerHealth>().AddLifePlayer(1);
            Destroy(collision.gameObject);
        }else if (collision.CompareTag("Armor"))
        {
            GetComponent<PlayerHealth>().currentArmor += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("FullHeart"))
        {

            GetComponent<PlayerHealth>().AddLifePlayer(1);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Currency"))
        {
            inventory.goldPlayer += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("AmoCase"))
        {
            if (gameObject.GetComponentInChildren<WeaponManager>().GetComponentInChildren<DistanceWeapon>())
            {
                DistanceWeapon weapon = gameObject.GetComponentInChildren<WeaponManager>().GetComponentInChildren<DistanceWeapon>();
                weapon.AmmoStock += numberOfAmoInCase;
                Destroy(collision.gameObject);
            }
            if (gameObject.GetComponentInChildren<WeaponManager>().GetComponentInChildren<CacWeapons>())
            {
                return;
            }
        }

    }


}

