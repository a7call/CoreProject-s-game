using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAbility : IAbility
{
    private float _stunTime;
    public StunAbility(float stunTime)
    {
        _stunTime = stunTime;
    }
    
    public void ApplyEffect(Characters character)
    {
        CoroutineManager.Instance.StartCoroutine(StunCO(character, _stunTime)) ;
    }
    private IEnumerator StunCO(Characters character, float stunTime)
    {
        character.IsStuned = true;
        yield return new WaitForSeconds(stunTime);
        character.IsStuned = false;
    }
}
