using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public List<Component> weapons;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<Component> weapons = new List<Component>();
        GameObject.FindGameObjectWithTag("WeaponManager").GetComponents(typeof(Weapons), weapons);
        if (collision.CompareTag("Player"))
        {
            foreach(Weapons weapon in weapons)
            {
                weapon.enabled = false;
            }
            collision.transform.GetChild(1).GetComponent<Masse>().enabled = true;
            Destroy(gameObject);
        }
    }
}
