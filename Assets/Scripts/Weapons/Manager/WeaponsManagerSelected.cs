﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Weapon manager, gére le script actif en fontion de l'arme ramassé
/// </summary>
public class WeaponsManagerSelected : MonoBehaviour
{
    // On le met à moins 1 car la première arme ramassée correspond au 0 et non pas 1
    [HideInInspector]
    public int selectedCacWeapon = -1;
    [HideInInspector]
    public int selectedDistanceWeapon = -1;

    public List<GameObject> cacWeaponsList = new List<GameObject>();
    public List<GameObject> distanceWeaponsList = new List<GameObject>();

    private bool isPlayingCac=false;
    private bool isPlayingDistance=false;

    public Sprite cacSprite;
    public Sprite distanceSprite;
    public string ammoText;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        SwitchCacToDistance();
        SwitchDistanceToCac();
        ChangeWeapons();
        //WhichWeaponScroll();
        UpdateUIWeapon();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CacWeapon") || collision.CompareTag("DistanceWeapon"))
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
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                transform.GetChild(transform.childCount-1).gameObject.SetActive(true);
            }

            if (collision.CompareTag("CacWeapon"))
            {
                isPlayingCac = true;
                isPlayingDistance = false;
                selectedCacWeapon++;
                cacWeaponsList.Add(collision.gameObject);
            }
            else
            {
                isPlayingDistance = true;
                isPlayingCac = false;
                selectedDistanceWeapon++;
                distanceWeaponsList.Add(collision.gameObject);
            } 
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        int j = 0;

        if (isPlayingCac == true)
        {
            foreach (GameObject weapon in cacWeaponsList)
            {
                if (i == selectedCacWeapon)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
                i++;
            }
            foreach(GameObject weapon in distanceWeaponsList)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        if (isPlayingDistance == true)
        {
            foreach (GameObject weapon in distanceWeaponsList)
            {
                if (j == selectedDistanceWeapon)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
                j++;
            }
            foreach (GameObject weapon in cacWeaponsList)
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    // Actuellement, il appuie sur deux touches différentes pour choisir le mode
    private void SwitchCacToDistance()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isPlayingCac = false;
            isPlayingDistance = true;

                for (int i = 0; i < distanceWeaponsList.Count; i++)
                {
                    if (i == selectedDistanceWeapon) distanceWeaponsList[i].gameObject.SetActive(true);
                    else distanceWeaponsList[i].gameObject.SetActive(false);
                }

                foreach (GameObject cacWeapon in cacWeaponsList)
                {
                    cacWeapon.gameObject.SetActive(false);
                }
        }
    }

    private void SwitchDistanceToCac()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isPlayingCac = true;
            isPlayingDistance = false;

                for (int i = 0; i < cacWeaponsList.Count; i++)
                {
                    if (i == selectedCacWeapon) cacWeaponsList[i].gameObject.SetActive(true);
                    else cacWeaponsList[i].gameObject.SetActive(false);
                }

                foreach (GameObject distanceWeapon in distanceWeaponsList)
                {
                    distanceWeapon.gameObject.SetActive(false);
                }
        }
    }

    private void ChangeWeapons()
    {
        if (isPlayingCac == true)
        {
            int previousSelectedWeapon = selectedCacWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedCacWeapon >= cacWeaponsList.Count - 1)
                {
                    selectedCacWeapon = 0;
                }
                else
                {
                    selectedCacWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedCacWeapon <= 0)
                {
                    selectedCacWeapon = cacWeaponsList.Count - 1;
                }
                else
                {
                    selectedCacWeapon--;
                }
            }
            if (previousSelectedWeapon != selectedCacWeapon)
            {
                SelectWeapon();
            }
        }
        else if (isPlayingDistance == true)
        {
            int previousSelectedWeapon = selectedDistanceWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedDistanceWeapon >= distanceWeaponsList.Count - 1)
                {
                    selectedDistanceWeapon = 0;
                }
                else
                {
                    selectedDistanceWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedDistanceWeapon <= 0)
                {
                    selectedDistanceWeapon = distanceWeaponsList.Count - 1;
                }
                else
                {
                    selectedDistanceWeapon--;
                }
            }
            if (previousSelectedWeapon != selectedDistanceWeapon)
            {
                SelectWeapon();
            }
        }
    }

    // Méthode générique non utilisée mais conservée [A utiliser potentiellement si le code ci dessus est trop lourd]
    //private void ScrollWeapons(int _selectedWeapon, List<GameObject> _selectedWeaponList)
    //{
    //    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
    //    {
    //        if (_selectedWeapon >= _selectedWeaponList.Count - 1)
    //        {
    //            _selectedWeapon = 0;
    //        }
    //        else
    //        {
    //            _selectedWeapon++;
    //        }
    //    }

    //    if (Input.GetAxis("Mouse ScrollWheel") < 0f)
    //    {
    //        if (_selectedWeapon <= 0)
    //        {
    //            _selectedWeapon = _selectedWeaponList.Count - 1;
    //        }
    //        else
    //        {
    //            _selectedWeapon--;
    //        }
    //    }
    //}

    //private void WhichWeaponScroll()
    //{
    //    if (isPlayingCac == true)
    //    {
    //        print("Parcours liste cac");
    //        ScrollWeapons(selectedCacWeapon, cacWeaponsList);
    //    }
    //    else if (isPlayingDistance == true)
    //    {
    //        print("Parcours liste distance");
    //        ScrollWeapons(selectedDistanceWeapon, distanceWeaponsList);
    //    }
    //}

    public void UpdateUIWeapon()
    {
        for (int i = 0; i < cacWeaponsList.Count; i++)
        {
            if (i == selectedCacWeapon)
            {
                cacSprite = cacWeaponsList[i].GetComponent<CacWeapons>().image;
            }
        }
        for (int i = 0; i < distanceWeaponsList.Count; i++)
        {
            if (i == selectedDistanceWeapon)
            {
                distanceSprite = distanceWeaponsList[i].GetComponent<DistanceWeapon>().image;
                ammoText = distanceWeaponsList[i].GetComponent<DistanceWeapon>().BulletInMag.ToString();
            }
        }
    }
}