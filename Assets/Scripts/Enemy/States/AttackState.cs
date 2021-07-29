using System.Collections;
using UnityEditor;
using UnityEngine;

public class AttackState : AIState
{
    public AttackState(Enemy enemy) : base(enemy)
    {
        AICharacter = enemy;
    }

    public override IEnumerator StartState()
    {
        AICharacter.AIMouvement.ShouldMove = false;
        yield return null;
    }

    public override void UpdateState()
    {
        AICharacter.DoAttackingState();
    }
}
