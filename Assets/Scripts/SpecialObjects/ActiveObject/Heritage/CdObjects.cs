using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CdObjects : ActiveObjects
{
    [SerializeField] protected float cd;
    protected override IEnumerator WayToReUse()
    {
        yield return new WaitForSeconds(cd);
        readyToUse = true;
    }
}
