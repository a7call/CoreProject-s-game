using System.Collections;
using UnityEngine;

public class ImmolationModule : PassiveObjects
{
     int damageAmount;
     float duration;   
    private void Start()
    {
        IAbility BurningEffect = new BurnAbility(damageAmount, duration);
        PassiveObjectManager.currentAbilities.Add(BurningEffect);
    }
}
