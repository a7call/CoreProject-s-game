using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnAbility : DotAbility, IAbility
{
    public BurnAbility(float _damageAmount, float _duration) : base(_damageAmount, _duration) { }

    public override void ApplyEffect(Characters character)
    {
        if (!character.IsBurned)
        {
            CoroutineManager.GetInstance().StartCoroutine(DotCo(character, _damageAmount, _duration));
        }
          

    }
    protected override IEnumerator DotCo(Characters character, float damageAmount, float duration)
    {
        character.IsBurned = true;
        yield return base.DotCo(character, damageAmount, duration);
        character.IsBurned = false;
        yield return 0;
    }
}
