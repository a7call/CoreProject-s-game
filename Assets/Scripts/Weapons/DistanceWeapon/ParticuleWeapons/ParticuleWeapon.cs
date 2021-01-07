using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleWeapon : DistanceWeapon
{
    //[SerializeField] float LoadingDelay;
    [SerializeField] protected int count;
    protected bool IsToHot = false;
    protected bool IsCooling = false;
    [SerializeField] protected float coolingTime;
    [SerializeField] protected float coolingDelay;
    [SerializeField] protected int countMax;
    [SerializeField] protected float knockBackforce;
    [SerializeField] protected float knockBackTime;
    Vector3 dir;

    private float radius = 1f;


    protected override void Update()
    {
        base.Update();
        if (!isAttacking && !IsCooling)
        {
            StartCoroutine(CoolDelay());
        }
        if (OkToShoot && !IsToHot)
        {
            dir = (attackPoint.position - transform.position).normalized;

            RaycastHit2D[] hits = Physics2D.CircleCastAll(attackPoint.position, radius, Vector2.zero);

            
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                    CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(knockBackforce, dir, knockBackTime, enemyScript));
                    enemyScript.TakeDamage(damage);

                }
            }

        }
    }
    protected override IEnumerator Shoot()
    {
        if (!isAttacking && !IsToHot)
        {
            isAttacking = true;
            count++;
            yield return new WaitForSeconds(1f);
            isAttacking = false;
        }
        if (count >= countMax)
        {
            IsToHot = true;
            StartCoroutine(Cooling());
        }

    }

    protected IEnumerator Cooling()
    {
        yield return new WaitForSeconds(coolingTime);
        IsToHot = false;
        count = 0;
    }

    protected IEnumerator CoolDelay()
    {
        if (count > 0)
        {
            IsCooling = true;
            yield return new WaitForSeconds(coolingDelay);
            count--;
            IsCooling = false;
        }

    }



}
