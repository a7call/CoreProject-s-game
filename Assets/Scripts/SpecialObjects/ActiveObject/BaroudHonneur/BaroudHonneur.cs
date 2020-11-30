using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaroudHonneur : ActiveObjects
{
    private PlayerHealth playerHealth;

    protected override void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && readyToUse)
        {
            print("A");
        }
    }

    private void ActiveBaroudHonneur()
    {
        if (playerHealth.currentHealth == 0)
        {
            UseModule = true;
            StartCoroutine(playerHealth.InvincibilityFlash());
            StartCoroutine(playerHealth.InvincibilityDelay());
            GainHp();
            ModuleAlreadyUse = true;
            readyToUse=false
        }
    }

    private void GainHp()
    {
        int gainHp = 2;
        playerHealth.currentHealth += gainHp;
    }
}
