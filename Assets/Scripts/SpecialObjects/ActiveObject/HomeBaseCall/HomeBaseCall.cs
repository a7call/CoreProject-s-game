using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBaseCall : StacksObjects
{
    [SerializeField] private GameObject amoCase = null;
    private GameObject player;
    [SerializeField] private float SupplyDelay=0;

   

    
    protected override void Update()
    {
        base.Update();


        if (UseModule)
        {
            CoroutineManager.Instance.StartCoroutine(AmmoSupply());
            UseModule = false;
        }
    }

    protected virtual IEnumerator AmmoSupply()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(SupplyDelay);
        Instantiate(amoCase, player.transform.position, Quaternion.identity);
    }
}

