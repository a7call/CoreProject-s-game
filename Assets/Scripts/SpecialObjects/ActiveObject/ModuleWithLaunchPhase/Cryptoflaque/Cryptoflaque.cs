using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cryptoflaque : ActiveObjects
{

    [SerializeField] protected GameObject Flaque;

    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            GetMousePosition();
            UseModule = false;
            Instantiate(Flaque, MousePos, Quaternion.identity);
        }


    }

}

