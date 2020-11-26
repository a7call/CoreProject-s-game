using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBaseCall : ActiveObjects
{
    [SerializeField] private GameObject amoCase = null;
    private GameObject player;
    [SerializeField] private float SupplyDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    protected override void Update()
    {
        base.Update();

        if (UseModule && !ModuleAlreadyUse)
        {
            StartCoroutine(AmmoSupply());
            ModuleAlreadyUse = true;
        }
    }

    protected virtual IEnumerator AmmoSupply()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(SupplyDelay);
        Instantiate(amoCase, player.transform.position, Quaternion.identity);
        print("test");
    }
}

