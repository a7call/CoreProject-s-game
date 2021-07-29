using System.Collections;
using UnityEditor;
using UnityEngine;

public class PatrollingState : AIState
{
    public PatrollingState(Enemy enemy) : base(enemy)
    {
    }
    public override IEnumerator StartState()
    {
        yield return null;
    }

    public override void UpdateState()
    {
        AICharacter.DoPatrollingState();
    }
}
