﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautExplose : Cac
{
    [SerializeField] private float timeToExplode = 1f;

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {

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
   
   
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(timeToExplode);
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);

        foreach (Collider2D h in hits)
        {
            if(h.CompareTag("Player")) h.GetComponent<PlayerHealth>().TakeDamage(1);
        }

        Destroy(gameObject);
       
    }
    protected override void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
            isReadyToSwitchState = false;
        }
    }


}