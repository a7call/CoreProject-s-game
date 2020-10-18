using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bubble360 : Distance
{
    private float radius;
    private float coeffDir = 1;
    protected bool damageDone = false;
    [SerializeField] protected LayerMask hitLayer;
    [CustomEditor(typeof(Enemy))]

    void Start()
    {
        currentState = State.Chasing;
        // Set premier targetPoint
        SetFirstPatrolPoint();
        // Set data
        SetData();
        SetMaxHealth();
    }
    protected override void Update()
    {


        base.Update();
        switch (currentState)
        {
            case State.Chasing:
                Aggro();
                isInRange();
                MoveToPath();
                break;
            case State.Attacking:
                UploadRadius();
                isInRange();
                //StartCoroutine("CanShoot");
                break;
        }

    }

    private float UploadRadius()
    {
        radius = coeffDir * Time.deltaTime;
        return radius;
    }

    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement

    // Override(Enemy.cs) Aggro s'arrete pour tirer et suit le player si plus à distance
    protected override void Aggro()
    {
        targetPoint = target;
    }

    protected override void PlayerInSight()
    {
        base.PlayerInSight();
    }

    protected override void isInRange()
    {
        base.isInRange();
    }

    // Voir Enemy.cs (héritage)
    protected override void Patrol()
    {
        base.Patrol();
    }

    // Voir Enemy.cs (héritage)
    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }


    // Health


    // Voir Enemy.cs (héritage)
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    // Voir Enemy.cs (héritage)
    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    // Voir Enemy.cs (héritage)
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }


    // Attack

    // Voir Enemy.cs (héritage)
    protected override IEnumerator CanShoot()
    {
        radius = 0f;
        return base.CanShoot();
    }

    // Voir Enemy.cs (héritage)
    protected override void ResetAggro()
    {
        base.ResetAggro();
    }


    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        rb.velocity = Vector2.zero;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 2, Vector2.zero, Mathf.Infinity, hitLayer);
        print(hit.transform.position);
        hit.transform.GetComponent<PlayerHealth>().TakeDamage(20);
        //Handles.color = Color.red;
        //Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), UploadRadius());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, UploadRadius());
    }

}
