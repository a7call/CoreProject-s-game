using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class SnotZone : Distance
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask hitLayer;
    private bool hasStartAttacking = true;

    protected void Start()
    {
        currentState = State.Patrolling;
        // Set data
        SetData();
        SetMaxHealth();
    }
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                StartCoroutine(Zone());
                // suit le path créé et s'arrête pour tirer

                break;


        }

    }
    private IEnumerator Zone()
    {
        if (hasStartAttacking)
        {
            hasStartAttacking = false;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, hitLayer);

            foreach (Collider2D hit in hits)
            {
                hit.GetComponent<PlayerHealth>().TakeDamage(1);

            }
            yield return new WaitForSeconds(1f);
            hasStartAttacking = true;
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
    }

}
