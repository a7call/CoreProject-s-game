using System.Collections;
using UnityEngine;


public class InterruptedState : AIState
{
    public InterruptedState(Enemy enemy) : base(enemy)
    {
        AICharacter = enemy;
    }
    public override IEnumerator EndState()
    {
        yield return null;
    }

    public override IEnumerator StartState()
    {
        CoroutineManager.GetInstance().StartCoroutine(InteruptAttack());
        yield return null;
        AICharacter.SetState(new ChasingState(AICharacter));
    }

    public override void UpdateState()
    {

    }

    private IEnumerator InteruptAttack()
    {
        CoroutineManager.GetInstance().StartCoroutine(AICharacter.RestCo(AICharacter.animator));
        yield return null;
    }
}
