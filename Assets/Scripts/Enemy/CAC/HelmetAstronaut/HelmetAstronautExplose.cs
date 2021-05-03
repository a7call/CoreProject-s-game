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
            case State.Chasing:
                isInRange();
                break;

            case State.Attacking:
                GetPlayerPos();
                StartCoroutine(TriggerExplosion());
                
                break;
        }
    }

    IEnumerator TriggerExplosion()
    {
        yield return new WaitForSeconds(timeToExplode);
        aIPath.canMove = false;
        Die();
    }


    private void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);
        
        foreach (Collider2D h in hits)
        {
            if(h.CompareTag("Player")) h.GetComponent<Player>().TakeDamage(1);
        }
    }
    protected override void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
            isReadyToSwitchState = false;
        }
    }
    public void PrintEvent(string s)
    {
        Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
    }

}
