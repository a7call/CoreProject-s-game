using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : DistanceWeapon
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




    protected override void Update()
    {
        base.Update();
        if (!isAttacking && !IsCooling)
        {
            StartCoroutine(CoolDelay());
        }
        if (OkToShoot && !IsToHot)
        {
            print("teet");
            dir = (attackPoint.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, dir, Mathf.Infinity, enemyLayer);

            Debug.DrawRay(attackPoint.position, dir * 20, Color.red);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy")) 
            {
                Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(knockBackforce, dir, knockBackTime, enemyScript));
                enemyScript.GetComponent<Enemy>().TakeDamage(damage);
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
            StartCoroutine(LaserCooling());
        }
       
    }

    protected IEnumerator LaserCooling()
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
