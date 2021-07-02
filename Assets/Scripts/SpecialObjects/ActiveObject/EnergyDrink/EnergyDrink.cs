using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : StacksObjects
{


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            UseModule = false;
            RefillEnergy();
        }
    }



    private void RefillEnergy()
    {
        
        Player energy = player.gameObject.GetComponent<Player>();
        
    }



}
