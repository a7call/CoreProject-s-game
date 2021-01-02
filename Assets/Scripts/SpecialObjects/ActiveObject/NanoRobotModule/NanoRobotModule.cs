using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoRobotModule : CdObjects
{

    public static List<Enemy> enemiesTouched = new List<Enemy>();
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float damage = 0f;
    protected override void Start()
    {
        base.Start();
        PlayerProjectiles.isNanoRobotModule = true;
    }
    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            ActivateNanoRobot();
            UseModule = false;
        }
    }

    private void ActivateNanoRobot()
    {
        foreach(Enemy enemy in enemiesTouched)
        {
            if (enemy == null) continue;
            Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.transform.position, range, enemyLayer);
            foreach(Collider2D hit in hits)
            {
                if (enemy == null) continue;
                Enemy enemyScript = hit.GetComponent<Enemy>();
                enemyScript.TakeDamage(damage);
                Vector2 dir = (enemy.gameObject.transform.position - hit.gameObject.transform.position).normalized;
                CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(200, -dir, 0.2f, enemy));
            }
               
        }
        enemiesTouched.Clear();
    }


    public void NanoRobotExplosion(Transform enemy)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.position, range, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == null) continue;
            if (hit.CompareTag("Enemy"))
            {
                if (hit.gameObject == enemy.gameObject) continue;
                Enemy enemyScript = hit.GetComponent<Enemy>();
                enemyScript.TakeDamage(damage);
                Vector2 dir = (enemy.transform.position - hit.gameObject.transform.position).normalized;
                CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(200, -dir, 0.2f, enemyScript));
            }
        }
    }

}
