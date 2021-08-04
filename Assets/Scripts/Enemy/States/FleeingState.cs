using System.Collections;
using UnityEditor;
using UnityEngine;
using Wanderer.Utils;

public class FleeingState : AIState
{
    public FleeingState(Enemy enemy) : base(enemy)
    {
    }
    public override IEnumerator StartState()
    {
        AICharacter.AIMouvement.ShouldMove = true;
        yield return Flee();
        AICharacter.SetState(new ChasingState(AICharacter));
    }

    public override void UpdateState()
    {
    }

    //to Rework
    private IEnumerator Flee()
    { 
        bool isValid = false;
        Vector3 randomPointPos = Vector3.zero;
        float fleeingRadius = 6;
        
        while (!isValid)
        {
            
            Vector3 randomPoint = fleeingRadius * Random.insideUnitCircle;
            randomPointPos = Utils.RandomPointInBounds(AICharacter.roomFloorCollider.bounds, 1f);
             isValid = true;
           
        }

        GameObject targetPoint = new GameObject();

        AICharacter.AIMouvement.target = targetPoint.transform;
        AICharacter.AIMouvement.ShouldSearch = true;
        AICharacter.AIMouvement.ShouldMove = true;

        while ((Vector3.Distance(AICharacter.transform.position, targetPoint.transform.position) > 1f))
        {
            yield return null;
        }
        AICharacter.AIMouvement.target = AICharacter.transform;
        GameObject.Destroy(targetPoint);
    }
}
