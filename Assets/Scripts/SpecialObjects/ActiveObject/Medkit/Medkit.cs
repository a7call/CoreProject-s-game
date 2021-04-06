using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : StacksObjects
{

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
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
        if(player.currentHealth != player.maxHealth)
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
        player.currentHealth = player.maxHealth;
    }

}
