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
            ActiveBaroudHonneur();
        }

        if (ModuleAlreadyUse)
        {
            Destroy(gameObject);
        }
    }

    private void ActiveBaroudHonneur()
    {
        if (playerHealth.currentHealth == 0)
        {
            UseModule = true;
            GainHp();
            // VOIR CE QU'ON FAIT ? RESET ROOM, INVINCIBLE QUELQUES SECONDES ?
            ModuleAlreadyUse = true;
            readyToUse = false;
        }
    }

    private void GainHp()
    {
        int gainHp = 2;
        playerHealth.currentHealth += gainHp;
    }
}
