using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Transform[] wayPoints;
     protected Transform targetPoint;
    [SerializeField] protected Transform targetToFollow;
    [SerializeField] protected float aggroDistance;
    private int index = 0;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private bool isPatroling;

    private void Start()
    {
        targetPoint = wayPoints[0];
    }

    private void Update()
    {
        Patrol();
        Aggro();
    }

    // Enemy patrol fonction
    protected void Patrol()
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
    protected  void Aggro()
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

}
