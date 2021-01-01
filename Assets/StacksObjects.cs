using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StacksObjects : ActiveObjects
{
   [SerializeField] protected int numberOfUse = 0;
   [SerializeField] protected float cd;
    private bool isOutOfUse = false;


    protected override IEnumerator WayToReUse()
    {
        numberOfUse--;
        if (numberOfUse < 1) isOutOfUse = true;
        if (!isOutOfUse)
        {
            yield return new WaitForSeconds(cd);
            readyToUse = true;
        }  
    }

    protected override void Update()
    {
        if (isOutOfUse) Destroy(gameObject);
    }


}
