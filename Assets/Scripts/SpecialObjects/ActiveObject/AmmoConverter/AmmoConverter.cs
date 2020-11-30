using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoConverter : ActiveObjects
{
    
    private GameObject player;
    [SerializeField] private float SupplyDelay = 0;




    protected override void Update()
    {
        base.Update();

        if (UseModule)
        {
            //StartCoroutine(AmmoSupply());
            UseModule = false;
        }



    }

    //protected void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("EnemyProjectil"))
    //    {
    //        collision.Destroy(gameObject);
    //    }
    //}
}
