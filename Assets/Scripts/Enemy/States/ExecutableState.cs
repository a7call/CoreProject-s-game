using System.Collections;
using UnityEngine;


public class ExecutableState : AIState
{

    public ExecutableState(Enemy enemy) : base(enemy)
    {
        AICharacter = enemy;
    }

    public override IEnumerator EndState()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator StartState()
    {
        yield return null;
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
