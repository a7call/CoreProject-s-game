using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMouvement : MonoBehaviour
{

    [SerializeField] protected EnemyScriptableObject EnemyData;

     protected float moveSpeed;
    [SerializeField] protected Transform[] wayPoints;
     protected Transform targetPoint;
    [SerializeField] protected Transform targetToFollow;
     protected float aggroDistance;
     private int index = 0;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected bool isPatroling;

    protected void Awake()
    {
        SetData();
        targetPoint = wayPoints[0];
    }

    protected virtual void Update()
    {
        
        Patrol();
        Aggro();
    }

    // Enemy patrol fonction
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

    protected void SetData()
    {
        moveSpeed = EnemyData.moveSpeed;
        aggroDistance = EnemyData.aggroDistance;

    }

}
