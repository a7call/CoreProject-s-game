using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBaseCall : StacksObjects
{
    [SerializeField] private GameObject amoCase = null;
    [SerializeField] private float SupplyDelay=0;
    private Vector3 SupplyPosition = Vector3.zero;

   

    
    protected override void Update()
    {
        base.Update();


        if (UseModule)
        {
            CoroutineManager.GetInstance().StartCoroutine(AmmoSupply());
            UseModule = false;
            SupplyPosition = player.transform.position;
        }
    }

    protected virtual IEnumerator AmmoSupply()
    {
        
        yield return new WaitForSeconds(SupplyDelay);
        Instantiate(amoCase, SupplyPosition, Quaternion.identity);
    }
}

