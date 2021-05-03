using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAbility : DotAbility, IAbility
{
    public PoisonAbility(float _damageAmount, float _duration) : base(_damageAmount, _duration) { }

    public override void ApplyEffect(ICharacter character)
    {
        if (!character.IsPoisoned)
            CoroutineManager.Instance.StartCoroutine(DotCo(character, _damageAmount, _duration));

    }
    protected override IEnumerator DotCo(ICharacter character, float damageAmount, float duration)
    {
        character.IsPoisoned = true;
        yield return base.DotCo(character, damageAmount, duration);
        character.IsPoisoned = false;
        yield return 0;
    }
}
