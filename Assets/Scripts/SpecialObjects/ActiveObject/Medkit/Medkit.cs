using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : StacksObjects
{

    private PlayerHealth playerHealth;

    private float timeCantMoove = 1.5f;
    private bool canWalk = true;


    protected override void Start()
    {
        base.Start();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
           SoinFull();
            UseModule = false;
        }
    }
    protected override IEnumerator WayToReUse()
    {
        if(playerHealth.currentHealth != playerHealth.maxHealth)
        {
            numberOfUse--;
        }
        if (numberOfUse < 1)
        {
            isOutOfUse = true;
        }

        if (!isOutOfUse )
        {
            yield return new WaitForSeconds(cd);
            readyToUse = true;
        }
    }

    private void SoinFull()
    {
        playerHealth.currentHealth = playerHealth.maxHealth;
    }

}
