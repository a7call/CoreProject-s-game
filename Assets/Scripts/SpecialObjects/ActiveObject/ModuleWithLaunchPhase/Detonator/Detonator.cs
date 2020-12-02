using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : ModuleLauchPhase
{
    [SerializeField] private float timeBeforDesactivation;
    [SerializeField] private float radius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private LayerMask hit;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;
    [SerializeField] protected float exploDelay;
    [SerializeField] protected bool readyToUse = false;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(ExploDelay());
        
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.U) && readyToUse)
        {
            Explosion();
            readyToUse = false;

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


    protected IEnumerator ExploDelay()
    {
        yield return new WaitForSeconds(exploDelay);
        readyToUse = true;
    }
    //protected override void Update()
    //{
    //    base.Update();
    //    if (isNotMoving && !isAlreadyActive)
    //    {
    //        isAlreadyActive = true;
    //        StartCoroutine(Explosion());
    //    }
    //}
}