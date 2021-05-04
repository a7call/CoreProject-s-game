using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWithWeapon : Distance
{
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Chasing:
                isInRange();
                // suit le path cr�� et s'arr�te pour tirer
                break;
            case State.Attacking:
                isInRange();
                // Couroutine g�rant les shoots 
                StartCoroutine(CanShootCO());
                break;
        }

    }
}
