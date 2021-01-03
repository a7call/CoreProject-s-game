using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidGrenade : ExplosivesModule
{
    [SerializeField] private float timeBeforExplosion;
    [SerializeField] private float SlowTime;
    [SerializeField] protected float SpeedDivisor;
    List<Enemy> ennemyTouche = new List<Enemy>();

    protected override void Start()
    {
        base.Start();
        CoroutineManager.Instance.StartCoroutine(ExplosionOnEnemy());
    }

    protected override void ExplosionEffects(Enemy enemy)
    {
         ennemyTouche.Add(enemy);
         Vector3 Direction = enemy.transform.position - gameObject.transform.position;
         enemy.TakeDamage(explosionDamage);
         CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, Direction, knockBackTime, enemy));
         enemy.moveSpeed /= SpeedDivisor;
         CoroutineManager.Instance.StartCoroutine(SlowEnemy());
         
    }

    protected override void Update()
    {
        base.Update();
    }
    private IEnumerator SlowEnemy()
    {
        
        yield return new WaitForSeconds(SlowTime);
       
        foreach (Enemy enemy in ennemyTouche)
        {
            if (enemy == null)
            {
                continue;
            }
            enemy.moveSpeed *= SpeedDivisor;
            
        }
        
    }

    
}
