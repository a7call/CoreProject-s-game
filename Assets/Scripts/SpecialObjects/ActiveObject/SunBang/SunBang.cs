using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBang : StacksObjects
{
    // Variables pour générer le cercle et voir si y'a des ennemis dans la zone
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private float stunTime = 5f;

    public List<Enemy> enemiesInRange = new List<Enemy>();

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            StartCoroutine(UseSunBang());
            UseModule = false;
        }

    }

    private IEnumerator UseSunBang()
    {
   
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, hitLayer);
        foreach (Collider2D hit in hits)
        {
            
            if (hit.CompareTag("Enemy"))
            {
                Enemy enemyScript = hit.GetComponent<Enemy>();
                enemiesInRange.Insert(0, enemyScript);
                enemyScript.currentState = Enemy.State.Paralysed;
                enemyScript.isreadyToAttack = false;

            }
        }
        yield return new WaitForSeconds(stunTime);

        foreach (Enemy enemy in enemiesInRange.ToArray()) 
        {
            if (enemy == null) continue;
            enemy.currentState = Enemy.State.Chasing;
            enemy.aIPath.canMove = true; 
            enemy.isreadyToAttack = true;
            enemiesInRange.Remove(enemy);
        }

    }

    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
