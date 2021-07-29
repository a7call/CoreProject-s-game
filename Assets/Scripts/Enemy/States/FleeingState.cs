using System.Collections;
using UnityEditor;
using UnityEngine;

public class FleeingState : AIState
{
    public FleeingState(Enemy enemy) : base(enemy)
    {
    }
    public override IEnumerator StartState()
    {
        AICharacter.AIMouvement.ShouldMove = true;
        yield return Flee();
        AICharacter.SetState(new AttackState(AICharacter));
    }

    public override void UpdateState()
    {
    }

    //to Rework
    private IEnumerator Flee()
    {
        Vector2 randomPoint = 5 * Random.insideUnitCircle;
        GameObject targetPoint = new GameObject();
        targetPoint.transform.position = AICharacter.transform.position + (Vector3)randomPoint;
        
        AICharacter.AIMouvement.ShouldMove = true;
        AICharacter.AIMouvement.ShouldSearch = true;
        AICharacter.AIMouvement.target = targetPoint.transform;
        if (AICharacter.AIMouvement.UpdatePathCo == null)
            yield break;

        while ((Vector3.Distance(AICharacter.transform.position, targetPoint.transform.position) > 0.5f))
        {
            Debug.Log(Vector3.Distance(AICharacter.transform.position, targetPoint.transform.position));
            yield return null;
        }
        AICharacter.AIMouvement.target = AICharacter.AIMouvement.Player;
        GameObject.Destroy(targetPoint);
    }
}
