using UnityEditor;
using UnityEngine;

public class AttackState : AIState
{
    public AttackState(Enemy enemy) : base(enemy)
    {
        AICharacter = enemy;
    }

    public override void StartState()
    {
        AICharacter.AIMouvement.ShouldMove = false;
    }

    public override void UpdateState()
    {
        AICharacter.DoAttackingState();
    }
}
