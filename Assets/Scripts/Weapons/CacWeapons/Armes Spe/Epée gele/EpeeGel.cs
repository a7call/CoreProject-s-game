using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpeeGel : CacWeapons
{
    public float SlowMultiplier;
    public float GelTimer;
    public static float SlowDiviser;
    public static float Timer;

    protected override void Awake()
    {
        base.Awake();
        SlowDiviser = SlowMultiplier;
        Timer = GelTimer;

    }

    protected override void AttackAppliedOnEnemy(Collider2D[] enemyHit)
    {
        

        foreach (Collider2D enemy in enemyHit)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.TakeDamage(damage);
                CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(knockBackForce, dir, knockBackTime, enemyScript));

                CoroutineManager.Instance.StartCoroutine(Gel(enemyScript));

            }
        }
    }

    public static IEnumerator Gel(Enemy enemy)
    {
        
        enemy.AIMouvement.Speed /= SlowDiviser;
        yield return new WaitForSeconds(Timer);
        enemy.AIMouvement.Speed *= SlowDiviser;

    }
}