using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontFuckWithMeModule : PassiveObjects
{
    [SerializeField] private float range;
     public static float Range;
    [SerializeField] private LayerMask enemyLayer;
     private Vector3 center;
     public static Vector3 Center;
     public static LayerMask EnemyLayer;
    void Start()
    {
        
        PlayerHealth.IsDontFuckWithMe = true;
        EnemyLayer = enemyLayer;
        Range = range;
        


    }
    private void Update()
    {
        center = transform.position;
        Center = center;
    }

    public static void DestroyAllEnemyInRange()
    {
        Collider2D[] ennemies = Physics2D.OverlapCircleAll(Center, Range, EnemyLayer);
        
        foreach (Collider2D enemy in ennemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                if (enemy.gameObject == null) continue;
                Destroy(enemy.gameObject);
            }
          
        }
    }




}