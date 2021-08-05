using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Wanderer.Utils;

public class FleeingState : AIState
{
    float _fleeingSpeed;
    float _fleeingDebuffTime;
    public FleeingState(Enemy enemy, float fleeingSpeed, float fleeingDebuffTime) : base(enemy)
    {
        _fleeingSpeed = fleeingSpeed;
        _fleeingDebuffTime = fleeingDebuffTime;
    }
    public override IEnumerator StartState()
    {
        yield return Flee(); 
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
        var speed = AICharacter.AIMouvement.Speed;
        AICharacter.AIMouvement.Speed = _fleeingSpeed;
        while (!isValid)
        {
            position = Utils.RandomPointInBounds(AICharacter.RoomFloorCollider.bounds, 1f);
            position.z = 0;

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
        AICharacter.AIMouvement.Speed = speed;
        GameObject.Destroy(targetPoint);
    }
    
    IEnumerator FleeingDebuff()
    {
        AICharacter.CanFlee = false;
        yield return new WaitForSeconds(_fleeingDebuffTime);
        AICharacter.CanFlee = true;
    }
}
