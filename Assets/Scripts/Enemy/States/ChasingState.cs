public class ChasingState : AIState
{
    public ChasingState(Enemy enemy) : base( enemy)
    {
    }

    public override void StartState()
    {
        AICharacter.AIMouvement.ShouldMove = true;
    }

    public override void UpdateState()
    {
        AICharacter.DoChasingState();
    }
}