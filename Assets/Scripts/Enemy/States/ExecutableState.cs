using System.Collections;
using UnityEngine;


public class ExecutableState : AIState
{
    private Coroutine ExecutionDelayCo;
    private float ExecutionDelay { get; set; } = 5f;
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
        AICharacter.AIMouvement.ShouldMove = false;
        yield return null;
        ExecutionDelayCo = CoroutineManager.GetInstance().StartCoroutine(ExecutionableCo());        
        AICharacter.CurrentHealth = 0;
       
    }

    public override void UpdateState()
    {
        if (AICharacter.CurrentHealth <= -10)
        {
            CoroutineManager.GetInstance().StopCoroutine(ExecutionDelayCo);
            AICharacter.IsExecutable = false;
            AICharacter.IsDying = true;
            AICharacter.SetState(new DeathState(AICharacter));
        }
    }

   

    IEnumerator ExecutionableCo()
    {
        yield return new WaitForSeconds(ExecutionDelay);
        AICharacter.IsExecutable = false;
        AICharacter.SetState(new RecoveryState(AICharacter));
    }

}
