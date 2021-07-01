using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceFlamme : ParticuleWeapon
{
     private float timeBetwenDamage = 1f;
     private float igniteTime = 5f;
     private float igniteDamage = 0.5f;
    protected override void ActualShoot()
    {
        if (OkToShoot && !IsToHot)
        {
            if (!GetComponentInChildren<ParticleSystem>().isPlaying) GetComponentInChildren<ParticleSystem>().Play();
            dir = (attackPoint.position - transform.position).normalized;

            RaycastHit2D[] hits = Physics2D.CircleCastAll(attackPoint.position, radius, Vector2.zero);


            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                    CoroutineManager.GetInstance().StartCoroutine(enemyScript.KnockCo(knockBackforce, dir, knockBackTime, enemyScript));

                    enemyScript.TakeDamage(damage);

                }
            }

        }
        else
        {
            if (GetComponentInChildren<ParticleSystem>().isPlaying) GetComponentInChildren<ParticleSystem>().Stop();

        }
    }

}
