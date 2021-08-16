using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Wanderer.Utils;

public class FleeingState : AIState
{
    float _fleeingSpeed;
    float _fleeingDebuffTime;
    float _minFleeDistance;
    public FleeingState(Enemy enemy, float fleeingSpeed, float fleeingDebuffTime, float minFleeDistance) : base(enemy)
    {
        _fleeingSpeed = fleeingSpeed;
        _fleeingDebuffTime = fleeingDebuffTime;
        _minFleeDistance = minFleeDistance;
    }

    public override IEnumerator EndState()
    {
        CoroutineManager.GetInstance().StartCoroutine(AICharacter.EndFleeingState());
        yield return null;   
    }
    public override IEnumerator StartState()
    {
        yield return Flee();
        AICharacter.AIMouvement.ShouldMove = false;
        CoroutineManager.GetInstance().StartCoroutine(FleeingDebuff()); 
        AICharacter.SetState(new ChasingState(AICharacter));
    }

    public override void UpdateState()
    {
    }

    //to Rework
    private IEnumerator Flee()
    {
        bool isValid = false;
        Vector3 position = Vector3.zero;
        var speed = AICharacter.AIMouvement.MoveForce;
        AICharacter.AIMouvement.MoveForce = _fleeingSpeed;
        while (!isValid)
        {
            yield return null;
            position = Utils.RandomPointInBounds(AICharacter.RoomFloorCollider.bounds, 1f);
            position.z = 0;

            if (Vector3.Distance(position, AICharacter.transform.position) < _minFleeDistance)
            {
                continue;
            }

            if (!Utils.IsPointWithinCollider(AICharacter.RoomFloorCollider, position))
            {   
                continue;
            }

            if (Physics2D.OverlapCircleAll(position, 0.5f).Any(x => !x.isTrigger))
            {
                continue;
            }

            isValid = true;
        }

        GameObject targetPoint = new GameObject("FleeingPosition" + AICharacter.gameObject.name);
        targetPoint.transform.position = (Vector2)position;
        AICharacter.AIMouvement.target = targetPoint.transform;
        AICharacter.AIMouvement.ShouldSearch = true;
        AICharacter.AIMouvement.ShouldMove = true;

        while ((Vector3.Distance(AICharacter.transform.position, targetPoint.transform.position) > 1f))
        {
            yield return null;
        }

        AICharacter.AIMouvement.target = AICharacter.transform;
        AICharacter.AIMouvement.MoveForce = speed;
        GameObject.Destroy(targetPoint);
    }
    
    IEnumerator FleeingDebuff()
    {
        yield return new WaitForSeconds(_fleeingDebuffTime);
        AICharacter.CanFlee = true;
    }
}
