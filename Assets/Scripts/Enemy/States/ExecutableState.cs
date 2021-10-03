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
        yield return null;
        CoroutineManager.GetInstance().StartCoroutine(AICharacter.SwitchToExecutionableState());
        CoroutineManager.GetInstance().StartCoroutine(AICharacter.RestCo(AICharacter.animator));
        AICharacter.AIMouvement.ShouldMove = false;
        ExecutionDelayCo = CoroutineManager.GetInstance().StartCoroutine(ExecutionableCo());        
        AICharacter.CurrentHealth = 0;      
    }

    public override void UpdateState()
    {
    }

   

    IEnumerator ExecutionableCo()
    {
        yield return new WaitForSeconds(ExecutionDelay);
        AICharacter.IsExecutable = false;
        AICharacter.SetState(new RecoveryState(AICharacter));
    }

}
