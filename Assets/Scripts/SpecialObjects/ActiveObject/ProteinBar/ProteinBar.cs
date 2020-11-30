using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinBar : ActiveObjects
{

    private PlayerHealth playerHealth;

    protected override void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    protected override void Update()
    {
        if(Input.GetKeyDown(KeyCode.U) && readyToUse)
        {
            TakeProteinBar();
        }

        if (ModuleAlreadyUse)
        {
            DestroyObject();
        }
    }

    private void TakeProteinBar()
    {
        int gainHp = 2;
        UseModule = true;
        playerHealth.currentHealth += gainHp;
        ModuleAlreadyUse = true;
        readyToUse = false;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }


}
