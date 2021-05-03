using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DotAbility 
{

    protected float _damageAmount;
    protected float _duration;

    public DotAbility( float damageAmount, float duration)
    {
        _damageAmount = damageAmount;
        _duration = duration;
    }
    public abstract void ApplyEffect(ICharacter character);

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
            character.TakeDamage(damagePerLoop);
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
    }


}
