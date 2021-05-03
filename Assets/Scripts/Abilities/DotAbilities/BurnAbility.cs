using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnAbility : DotAbility, IAbility
{
    public BurnAbility(float _damageAmount, float _duration) : base(_damageAmount, _duration) { }

    public override void ApplyEffect(ICharacter character)
    {
        if (!character.IsBurned)
        {
            CoroutineManager.Instance.StartCoroutine(DotCo(character, _damageAmount, _duration));
        }
          

    }
    protected override IEnumerator DotCo(ICharacter character, float damageAmount, float duration)
    {
        character.IsBurned = true;
        yield return base.DotCo(character, damageAmount, duration);
        character.IsBurned = false;
        yield return 0;
    }
}
