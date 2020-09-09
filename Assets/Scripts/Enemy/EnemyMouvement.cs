using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    protected float moveSpeed;
    protected Transform[] wayPoints;
    protected Transform targetPoint;
    protected Transform targetToFollow;
    protected float aggroDistance;
    private int index = 0;
    private Rigidbody2D rb;
    protected bool isPatroling;

    private void Start()
    {
        targetPoint = wayPoints[0];
    }


    protected virtual void Patrol()
    {
        Vector3 dir = (targetPoint.position - transform.position).normalized;
        rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
        if (Vector3.Distance(transform.position, targetPoint.position) < 1f && isPatroling )
        {
            index = (index + 1) % wayPoints.Length;
            targetPoint = wayPoints[index];

        }
    }

    protected virtual void Aggro()
    {
        if (Vector3.Distance(transform.position, targetToFollow.position) < aggroDistance)
        {
            isPatroling = false;
            Vector3 dir = (targetToFollow.position - transform.position).normalized;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            isPatroling = true;
            return;
        }
    }

}
