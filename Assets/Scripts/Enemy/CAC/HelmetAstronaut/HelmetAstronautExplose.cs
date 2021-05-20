using System.Collections;
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
                StartCoroutine(TriggerExplosion()); 
                break;
        }
    }

    IEnumerator TriggerExplosion()
    {
       yield return new WaitForSeconds(timeToExplode);
       AIMouvement.ShouldMove = false;
       Die();
    }

    // Similar to ApplyDamage but for Die animation event
    void OnDeathDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, AttackRadius, HitLayers);

        foreach (Collider2D h in hits)
        {

            if (h.CompareTag("Player"))
            {
                h.GetComponent<Player>().TakeDamage(1);
            }

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

}
