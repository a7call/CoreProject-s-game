using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : ModuleLauchPhase
{
    [SerializeField] private float radius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private LayerMask hit;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;
    DetonatorModule detonatorModule;


    protected override void Start()
    {
        base.Start();
        detonatorModule = FindObjectOfType<DetonatorModule>();
        
    }

    protected override void Update()
    {
        base.Update();
        if (detonatorModule.readyToExplode && detonatorModule.UseModule)
        {
            Explosion();
            detonatorModule.readyToExplode = false;
            detonatorModule.UseModule = false;
            if (detonatorModule.numberOfUse < 1)
            {
                detonatorModule.isOutOfUse = true;
            }
        }
    }


    private void Explosion()
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hit);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                Vector3 Direction = (enemy.transform.position - gameObject.transform.position).normalized;
                CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, Direction, knockBackTime, enemy));
                enemy.TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
    }

}