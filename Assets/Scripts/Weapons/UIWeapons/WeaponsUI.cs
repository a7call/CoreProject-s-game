using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponsUI : MonoBehaviour
{
    private WeaponsManagerSelected weaponManager;

    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponsManagerSelected>();
    }
    private void Update()
    {
       // SetUI();
       // SetUIAmmo();
    }

    private void SetUI()
    {
        if (gameObject.CompareTag("CacWeapon"))
        {
            gameObject.GetComponent<Image>().sprite = weaponManager.cacSprite; ;
        }
        else if (gameObject.CompareTag("DistanceWeapon"))
        {
            gameObject.GetComponent<Image>().sprite = weaponManager.distanceSprite;
        }
    }

    private void SetUIAmmo()
    {
        if(gameObject.GetComponent<Text>() != null)
        {
        gameObject.GetComponent<Text>().text = weaponManager.ammoText;
        }
    }
}
