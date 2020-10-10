using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Weapon manager, gére le script actif en fontion de l'arme ramassé
/// </summary>
public class WeaponManager : MonoBehaviour
{
    // Quand une arme est ramassé récupere son tag et active le script en relation 
    /* public void PickUpWeapon(GameObject obj)
     {
         switch (obj.tag)
         {
             default:
                 Debug.LogWarning("tag not supported");
                 break;
             case WeaponTags.MASSE_TAG:
                 GetComponent<Masse>().enabled = true;
                 break;
             case WeaponTags.FAUX_TAG:
                 GetComponent<Faux>().enabled = true;
                 break;

         }
     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            collision.transform.parent = gameObject.transform;
            collision.transform.position = gameObject.transform.position;
        }
    }

}
