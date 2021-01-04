using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteIdol : CdObjects
{
    [SerializeField] protected LayerMask enemyLayer;
    public static bool parasiteIdolFear = false;
    [SerializeField] private float fearTime = 5f;
    Collider2D[] hits;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(UseModule)
        {
            UseModule = false;
            CoroutineManager.Instance.StartCoroutine(ResetStateChasing());
        }
    }

    private void FearEnemy()
    {
        hits = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        foreach (Collider2D enemy in hits)
        {
            enemy.gameObject.GetComponent<Enemy>().currentState = Enemy.State.Feared;
        }
    }
    private void EnemyChasing()
    {
        foreach (Collider2D enemy in hits)
        {
            if (enemy == null) continue;
            enemy.gameObject.GetComponent<Enemy>().aIPath.canMove = true;
            enemy.gameObject.GetComponent<Enemy>().currentState = Enemy.State.Chasing;
            enemy.gameObject.GetComponent<Enemy>().rb.velocity = Vector3.zero;
            enemy.gameObject.GetComponent<Enemy>().direction = Vector3.zero;
        }
    }

    private IEnumerator ResetStateChasing()
    {
        FearEnemy();
        yield return new WaitForSeconds(fearTime);
        EnemyChasing();
    }


}
