using UnityEditor;
using UnityEngine;

public class PatrollingState : AIState
{
    public PatrollingState(Enemy enemy) : base(enemy)
    {
    }
    public override void StartState()
    {
        return;
    }

    public override void UpdateState()
    {
        AICharacter.DoPatrollingState();
    }
}
