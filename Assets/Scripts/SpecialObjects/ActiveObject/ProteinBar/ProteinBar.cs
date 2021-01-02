using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinBar : StacksObjects
{

    private PlayerHealth playerHealth;

    protected override void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    protected override void Update()
    {
        base.Update();
        if(UseModule)
        {
            TakeProteinBar();
            UseModule = false;
        }
    }

    private void TakeProteinBar()
    {
        int gainHp = 2;
        playerHealth.currentHealth += gainHp;
    }

}
