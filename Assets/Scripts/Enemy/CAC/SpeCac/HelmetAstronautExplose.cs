using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautExplose : Cac
{
    [SerializeField] private float timeToExplode = 1f;

    

    private void Start()
    {
        

        currentState = State.Patrolling;
        // Get Player Reference
        FindPlayer();
        // Set target
        targetPoint = target;
        // Set data
        SetData();
        SetMaxHealth();
    }

    protected override void Update()
    {

        AliveOrNot();

        base.Update();
        switch (currentState)
        {
            default:
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                isInRange();
                break;

            case State.Attacking:
                GetPlayerPos();
                StartCoroutine(Attack());
                break;
        }

    }

    private void AliveOrNot()
    {
        if (currentHealth <= 0) BaseAttack();
    
    }

    // Find player to follow
    private void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
   

    private IEnumerator Attack()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(timeToExplode);
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);

        foreach (Collider2D h in hits)
        {
            playerHealth.TakeDamage(1);
        }

        Destroy(gameObject);
       
    }


}
