using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidGrenade : ModuleLauchPhase
{
    [SerializeField] private float timeBeforExplosion;
    [SerializeField] private float SlowTime;
    [SerializeField] private float radius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private LayerMask hit;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;
    [SerializeField] protected float SpeedDivisor;
    private float EnnemyMoveSpeed=0;
    List<Enemy> ennemyTouche = new List<Enemy>();

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Explosion());
    }
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(timeBeforExplosion);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hit);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                ennemyTouche.Add(enemy);
                Vector3 Direction = enemy.transform.position - gameObject.transform.position;
                enemy.TakeDamage(explosionDamage);
                CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, Direction, knockBackTime, enemy));
                
                enemy.moveSpeed /= SpeedDivisor;
            }
        }
        CoroutineManager.Instance.StartCoroutine(SlowEnemy());

        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        if (isNotMoving && !isAlreadyActive)
        {
            isAlreadyActive = true;
            StartCoroutine(Explosion());
            //StartCoroutine(SlowEnemy());
        }
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
