using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extincteur : ParticuleWeapon
{
    [SerializeField] private float slowTimer;
    [SerializeField] private float slowMultiplier;

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
                    CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(knockBackforce, dir, knockBackTime, enemyScript));
                    CoroutineManager.Instance.StartCoroutine(SlowCo(enemyScript));
                    enemyScript.TakeDamage(damage);

                }
            }

        }
        else
        {
            if (GetComponentInChildren<ParticleSystem>().isPlaying) GetComponentInChildren<ParticleSystem>().Stop();

        }
    }


   protected IEnumerator SlowCo(Enemy enemy)
    {
        if (!enemy.IsSlowed)
        {
            enemy.IsSlowed = true;
            enemy.AIMouvement.Speed /= slowMultiplier;
            yield return new WaitForSeconds(slowTimer);
            if (enemy == null) yield break;
            enemy.AIMouvement.Speed *= slowMultiplier;
            enemy.IsSlowed = false;
        }
       
    }
}
