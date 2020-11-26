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

    public List<GameObject> cacWeaponsList = new List<GameObject>();
    public List<GameObject> distanceWeaponsList = new List<GameObject>();

    [SerializeField] private bool isPlayingCac=false;
    [SerializeField] private bool isPlayingDistance=false;


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
                print(weapon.name + " est désactivé");
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
                print(weapon.name + " est désactivé");
                weapon.gameObject.SetActive(false);
            }
        }
    }

    private void SwitchCacToDistance()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isPlayingCac = false;
            isPlayingDistance = true;
            //transform.GetChild(selectedCacWeapon).gameObject.SetActive(false);
            //transform.GetChild(selectedDistanceWeapon).gameObject.SetActive(true);
        }
    }

    private void SwitchDistanceToCac()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isPlayingCac = true;
            isPlayingDistance = false;
            //transform.GetChild(selectedDistanceWeapon).gameObject.SetActive(false);
            //transform.GetChild(selectedCacWeapon).gameObject.SetActive(true);
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
}