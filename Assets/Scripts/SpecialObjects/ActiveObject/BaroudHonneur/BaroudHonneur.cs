using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaroudHonneur : StacksObjects
{
    private PlayerHealth playerHealth;

    protected override void Start()
    {
        base.Start();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    protected override void Update()
    {
        
        if (playerHealth.currentHealth==0)
        {
            AutoBaroudHonneur();
            Destroy(gameObject);
            
        }

        if(UseModule)
        {
            ActiveManualBaroudHonneur();
        }
        base.Update();
    }

    private void ActiveManualBaroudHonneur()
    {
        GainHp();
    }

    private void AutoBaroudHonneur()
    {

        GainHp();
        CoroutineManager.Instance.StartCoroutine(playerHealth.InvincibilityDelay());
        CoroutineManager.Instance.StartCoroutine(playerHealth.InvincibilityFlash());
    }

    private void GainHp()
    {
        int gainHp = 2;
        playerHealth.currentHealth += gainHp;
    }
}
