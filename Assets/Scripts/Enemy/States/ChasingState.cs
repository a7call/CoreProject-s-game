using System.Collections;

public class ChasingState : AIState
{
    public ChasingState(Enemy enemy) : base( enemy)
    {
    }

    public override IEnumerator StartState()
    {
        AICharacter.AIMouvement.ShouldMove = true;
        AICharacter.AIMouvement.target = AICharacter.AIMouvement.Player;
        yield return null;
    }

    public override void UpdateState()
    {
        AICharacter.DoChasingState();
    }


}