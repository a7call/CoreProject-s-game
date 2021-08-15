using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeCacEnemy : ArmeEnemy
{
    // Start is called before the first frame update
    private Animator animator;
    private Cac enemyCac;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        enemyCac = GetComponentInParent<Cac>();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    void ApplyDamage()
    {
       
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, enemyCac.AttackRadius, enemyCac.HitLayers);

        foreach (Collider2D h in hits)
        {

            if (h.CompareTag("Player"))
            {
                h.GetComponent<Player>().TakeDamage(1);
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint != null) Gizmos.DrawWireSphere(attackPoint.position, enemyCac.AttackRadius);

    }
}
