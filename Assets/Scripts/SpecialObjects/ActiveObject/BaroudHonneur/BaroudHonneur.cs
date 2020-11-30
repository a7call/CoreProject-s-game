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
        if (readyToUse && playerHealth.currentHealth==0)
        {
            AutoBaroudHonneur();
        }

        if(Input.GetKeyDown(KeyCode.U) && readyToUse && playerHealth.currentHealth != 0)
        {
            ActiveManualBaroudHonneur();
        }

        if (ModuleAlreadyUse)
        {
            Destroy(gameObject);
        }
    }

    private void ActiveManualBaroudHonneur()
    {
        UseModule = true;
        GainHp();
        ModuleAlreadyUse = true;
        readyToUse = false;
    }

    private void AutoBaroudHonneur()
    {
        UseModule = true;
        GainHp();
        CoroutineManager.Instance.StartCoroutine(playerHealth.InvincibilityDelay());
        CoroutineManager.Instance.StartCoroutine(playerHealth.InvincibilityFlash());
        ModuleAlreadyUse = true;
        readyToUse = false;
    }

    private void GainHp()
    {
        int gainHp = 2;
        playerHealth.currentHealth += gainHp;
    }
}
