using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : Player
{
    WeaponsManagerSelected weaponManager;
    ActiveObjectManager activeObjectManager;
    Inventory inventory;
    protected bool isShooting=false;
    protected bool OpenCoffre = false;
    protected bool OpenShop = false;

    public PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        activeObjectManager = GetComponentInChildren<ActiveObjectManager>();
        weaponManager = GetComponentInChildren<WeaponsManagerSelected>();
        inventory = GetComponentInChildren<Inventory>();
    }
    public void OnShoot()
    {
        if (!isShooting)
        {
            isShooting = true;

            if (weaponManager.isPlayingDistance)
            {
                //weaponManager.GetComponentInChildren<DistanceWeapon>().toShoot();
                weaponManager.GetComponentInChildren<DistanceWeapon>().OkToShoot = true;
            }
            else if (weaponManager.isPlayingCac)
            {
                weaponManager.GetComponentInChildren<CacWeapons>().ToAttack();
            }
        }
        else
        {
            if (weaponManager.isPlayingDistance)
            {
                //weaponManager.GetComponentInChildren<DistanceWeapon>().toShoot();
                weaponManager.GetComponentInChildren<DistanceWeapon>().OkToShoot = false;
            }

            //print("StoopShoot");
            isShooting = false;

        }

        

    }

    public void OnReload()
    {
        if (weaponManager.isPlayingDistance)
        {
            weaponManager.GetComponentInChildren<DistanceWeapon>().toReload();
        }
    }


    public void OnUseObject()
    {
        if(activeObjectManager.GetComponentInChildren<ActiveObjects>())
        {
            activeObjectManager.GetComponentInChildren<ActiveObjects>().ToUseModule();
        }
    }

    public void OnBlackHole()
    {
        inventory.SpawnBlackHole();
    }

    public void OnOpenCoffre()
    {
        if (OpenCoffre)
        {
            OpenCoffre = false;
        }
        else
        {
            OpenCoffre = true;
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Coffre"))
        {
            if (OpenCoffre)
            {
                collision.GetComponent<Coffre>().OkToOpen = true;
            }
            else return;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Coffre"))
        {
            collision.GetComponent<Coffre>().OkToOpen = false;
        }
    }

    public void OnOpenShop()
    {
        ShopManager shopManager = FindObjectOfType<ShopManager>();

        if (OpenShop)
        {
            OpenShop = false;
            shopManager.OpenShop = false;
        }
        else
        {
            OpenShop = true;
            shopManager.OpenShop = true;
        }
    }

    public void OnPause()
    {
        PauseMenu.pause = true;
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }
}