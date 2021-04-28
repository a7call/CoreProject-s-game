using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotAbility 
{

    protected float _damageAmount;
    protected float _duration;

    public DotAbility( float damageAmount, float duration)
    {
        _damageAmount = damageAmount;
        _duration = duration;
    }

    protected virtual IEnumerator DotCo(ICharacter character, float damageAmount, float duration)
    {
        float amountDamaged = 0;
        float damagePerLoop = damageAmount / duration;
        while (amountDamaged < damageAmount)
        {
            if (character.Equals(null))
            {
                yield break;
            }
            character.TakeDamage(1);
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
    }


}
