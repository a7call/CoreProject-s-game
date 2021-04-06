using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaroudHonneur : StacksObjects
{
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        
        if (player.currentHealth==0)
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
        CoroutineManager.Instance.StartCoroutine(player.InvincibilityDelay());
        CoroutineManager.Instance.StartCoroutine(player.InvincibilityFlash());
    }

    private void GainHp()
    {
        int gainHp = 2;
        player.currentHealth += gainHp;
    }
}
