using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleWeapon : CollingWeapons
{
    protected Vector3 dir;
    protected float radius = 1f;


    //protected  void Update()
    //{
    //    base.Update();
    //    if (!isAttacking && !IsCooling)
    //    {
    //        StartCoroutine(CoolDelay());
    //    }
    //    ActualShoot();


    //}

    protected virtual void ActualShoot()
    {
        if (OkToShoot && !IsToHot)
        {
            if (!GetComponentInChildren<ParticleSystem>().isPlaying) GetComponentInChildren<ParticleSystem>().Play();

            //GetDirProj();

            RaycastHit2D[] hits = Physics2D.CircleCastAll(attackPoint.position, radius, Vector2.zero);


            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                    //CoroutineManager.GetInstance().StartCoroutine(enemyScript.KnockCo(knockBackforce, dirProj, knockBackTime, enemyScript));
                    enemyScript.TakeDamage(damage);

                }
            }

        }
        else
        {
            if (GetComponentInChildren<ParticleSystem>().isPlaying) GetComponentInChildren<ParticleSystem>().Stop();

        }
    }
    protected override IEnumerator Shooting()
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
