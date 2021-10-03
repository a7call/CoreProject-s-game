using System.Collections;
using UnityEngine;


public class RecoveryState : AIState
{
    public RecoveryState(Enemy enemy) : base(enemy)
    {
    }
    public override IEnumerator EndState()
    {
        yield return null;
        AICharacter.CanFlee = true;
        AICharacter.sr.material = AICharacter.BaseMaterial;
        AICharacter.CurrentHealth = AICharacter.MaxHealth / 2;
    }

    public override IEnumerator StartState()
    {
        AICharacter.SetState(new ChasingState(AICharacter));
        yield break;
    }

    public override void UpdateState()
    {
        return;
    }

}
