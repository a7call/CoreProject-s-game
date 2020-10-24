using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bubble360 : Distance
{
    private float radius = 0.5f;
    [SerializeField] private float coeffDir = 3f;
    protected bool damageDone = false;
    [SerializeField] protected LayerMask hitLayer;
    private PlayerHealth playerHealth;

    void Start()
    {

        playerHealth = FindObjectOfType<PlayerHealth>();
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
                StartCoroutine("CanShoot");
                break;
        }

    }

    private float UploadRadius()
    {
        radius += coeffDir * Time.deltaTime;
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
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }


    // Attack

    // Voir Enemy.cs (héritage)
    protected override void ResetAggro()
    {
        base.ResetAggro();
    }

    // Voir Enemy.cs (héritage)
    protected override IEnumerator CanShoot()
    {
        if (isShooting && isReadytoShoot)
        {
            isReadytoShoot = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }
    }


    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        rb.velocity = Vector2.zero;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 10, Vector2.zero, Mathf.Infinity, hitLayer);
        hit.transform.GetComponent<PlayerHealth>().TakeDamage(20);
        print(playerHealth.currentHealth);
        ////Handles.color = Color.red;
        ////Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), UploadRadius());
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, UploadRadius());
    //}

}
