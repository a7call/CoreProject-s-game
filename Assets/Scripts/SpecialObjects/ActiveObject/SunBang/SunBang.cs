using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBang : ActiveObjects
{
    // Variables pour générer le cercle et voir si y'a des ennemis dans la zone
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private float radiusZone = 5f;
    [SerializeField] private float stunTime = 5f;

    public List<Enemy> enemiesInRange = new List<Enemy>();

    protected override void Start()
    {

    }

    protected override void Update()
    {
        if (UseModule)
        {
            StartCoroutine(UseSunBang());
            UseModule = false;
        }

    }

    private IEnumerator UseSunBang()
    {
   
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radiusZone, hitLayer);
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
            enemy.currentState = Enemy.State.Chasing;
            enemy.aIPath.canMove = true; 
            enemy.isreadyToAttack = true;
            enemiesInRange.Remove(enemy);
        }

    }

    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusZone);
    }
}
