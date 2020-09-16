using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMouvement : MonoBehaviour
{

     protected float moveSpeed;
    [SerializeField] protected Transform[] wayPoints;
     protected Transform targetPoint;
    [SerializeField] protected Transform targetToFollow;
     protected float aggroDistance;
     private int index = 0;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected bool isPatroling;

   

    // Enemy patrol fonction
    protected virtual void Patrol()
    {
        if (isPatroling)
        {
            Vector3 dir = (targetPoint.position - transform.position).normalized;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;


            if (Vector3.Distance(transform.position, targetPoint.position) < 1f)
            {
                index = (index + 1) % wayPoints.Length;
                targetPoint = wayPoints[index];

            }
        }
    }

    // Enemy take Player aggro 
    protected virtual void Aggro()
    {
        Vector3 dir = (targetToFollow.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, targetToFollow.position) < aggroDistance)
        {
            isPatroling = false;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            isPatroling = true;
            return;
        }
    }

   protected virtual void SetFirstPatrolPoint()
    {
        targetPoint = wayPoints[0];
    }

}
