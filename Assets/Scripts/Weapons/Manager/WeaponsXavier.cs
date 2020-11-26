using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Weapon manager, gére le script actif en fontion de l'arme ramassé
/// </summary>
public class WeaponsXavier : MonoBehaviour
{
    public int selectedCacWeapon = 0;
    public int selectedDistanceWeapon = 0;
    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }


        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CacWeapon"))
        {
            collision.transform.parent = gameObject.transform;
            collision.GetComponent<Weapons>().enabled = true;
            collision.transform.position = gameObject.transform.position;
            collision.transform.gameObject.SetActive(false);
            collision.GetComponent<Collider2D>().enabled = false;
            if (transform.childCount == 1)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }


    private void SelectCacWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedCacWeapon && weapon.gameObject.tag=="CacWeapons")
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;

        }

    }

}