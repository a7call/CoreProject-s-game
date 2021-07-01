using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAbility : DotAbility, IAbility
{
    public PoisonAbility(float _damageAmount, float _duration) : base(_damageAmount, _duration) { }

    public override void ApplyEffect(Characters character)
    {
        if (!character.IsPoisoned)
            CoroutineManager.GetInstance().StartCoroutine(DotCo(character, _damageAmount, _duration));

    }
    protected override IEnumerator DotCo(Characters character, float damageAmount, float duration)
    {
        character.IsPoisoned = true;
        yield return base.DotCo(character, damageAmount, duration);
        character.IsPoisoned = false;
        yield return 0;
    }
}
