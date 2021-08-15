using System.Collections;
using UnityEngine;

public class ChasingState : AIState
{
    public ChasingState(Enemy enemy) : base(enemy)
    {
    }
    public override IEnumerator EndState()
    {
        yield return null;
    }
    public override IEnumerator StartState()
    {
        yield return null;
        AICharacter.StartChasingState();
        Debug.Log(AICharacter.AIMouvement.ShouldMove);
        AICharacter.AIMouvement.target = AICharacter.AIMouvement.Player;  
    }

    public override void UpdateState()
    {
        AICharacter.DoChasingState();
    }


}