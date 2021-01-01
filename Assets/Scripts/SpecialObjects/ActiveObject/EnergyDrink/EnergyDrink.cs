using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : StacksObjects
{


    protected override void Start()
    {
       
    }

    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            UseModule = false;
            CoroutineManager.Instance.StartCoroutine(UseEnergyDring());
        }
    }

    private IEnumerator UseEnergyDring()
    {
        yield return new WaitForSeconds(0.5f);
        print("test");
    }

    private void RefillEnergy()
    {
       
    }

    private void DestoyEnergyDrink()
    {
        Destroy(gameObject);
    }

}
