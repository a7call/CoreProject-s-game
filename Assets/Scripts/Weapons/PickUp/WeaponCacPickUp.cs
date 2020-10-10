using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe de pick up des armes Cac.
/// recupère la liste des armes, disable tout les scripts
/// et active le bon ( celui de l'arme ramassé)
/// </summary>
public class WeaponCacPickUp : MonoBehaviour
{
    // Liste des armes dans le weapon manager
    public List<Component> weapons;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<Component> weapons = new List<Component>();
        // rempli la liste des armes dans le weapon manager
        GameObject.FindGameObjectWithTag("WeaponManager").GetComponents(typeof(Weapons), weapons);
        if (collision.CompareTag("Player"))
        {
            // désactive tous les scripts d'arme
            foreach(Weapons weapon in weapons)
            {
                weapon.enabled = false;
            }
            WeaponManager playerWeaponManager = collision.transform.GetChild(1).GetComponent<WeaponManager>();
            // active le bon script en fonction de l'arme ramassé
          //  playerWeaponManager.PickUpWeapon(gameObject);
            
            Destroy(gameObject);
        }
    }
}
