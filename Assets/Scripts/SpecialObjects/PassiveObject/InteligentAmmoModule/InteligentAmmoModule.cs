using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteligentAmmoModule : PassiveObjects
{
    [SerializeField] public static float detectionRadius;
    [SerializeField] public float detectionR;
    [SerializeField] public static LayerMask enemyLayer;
    [SerializeField] public LayerMask enemyL;
    public static bool isDirUpdate;

    private void Awake()
    {
      
        enemyLayer = enemyL;
        detectionRadius = detectionR;
    }
    void Start()
    {
        PlayerProjectiles.isInteligentAmmoModule = true;
    }

   

    public static GameObject LockEnemy(GameObject proj)
    {
        Collider2D enemy = Physics2D.OverlapCircle(proj.transform.position, detectionRadius, enemyLayer);

        if (enemy != null)
        {
             GameObject lockedEnemy = enemy.gameObject;
             return lockedEnemy;
        }
        else
        {
            return null;
        }


    }
}
