using System.Collections;
using UnityEngine;


public class ExecutableState : AIState
{
    Coroutine ExecutionCo;
    public ExecutableState(Enemy enemy) : base(enemy)
    {
        AICharacter = enemy;
    }

    public override IEnumerator EndState()
    {
        yield return null;
    }

    public override IEnumerator StartState()
    {
        
        yield return null;
        CoroutineManager.GetInstance().StartCoroutine(Executionable());
    }

    public override void UpdateState()
    {
       
    }

   

    IEnumerator Executionable()
    {
        yield return new WaitForSeconds(4f);
        AICharacter.SetState(new DeathState(AICharacter));
    }

}
