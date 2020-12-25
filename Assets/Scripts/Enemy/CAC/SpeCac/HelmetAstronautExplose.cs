using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautExplose : Cac
{
    [SerializeField] private float timeToExplode = 1f;

    

    private void Start()
    {
        SetData();
        SetMaxHealth();
    }

    protected override void Update()
    {

        base.Update();
        switch (currentState)
        {
            default:
            case State.Patrolling:
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
   
    private IEnumerator Attack()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(timeToExplode);
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);

        foreach (Collider2D h in hits)
        {
            if(h.CompareTag("Player")) h.GetComponent<PlayerHealth>().TakeDamage(1);
        }

        Destroy(gameObject);
       
    }


}
