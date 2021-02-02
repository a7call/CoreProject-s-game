using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// à retaper entierement 

public class LaserSaber : CacWeapons
{
   
    protected override IEnumerator Attack()
    {
        if (!isAttacking && !PauseMenu.isGamePaused)
        {
            isAttacking = true;
            Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

            if (isVampirismeModule)
            {
                RewardSpawner.isAttackCAC = true;
            }

            AttackAppliedOnEnemy(enemyHit);
            
            SendBackProjectile(enemyHit);

            yield return new WaitForSeconds(attackDelay);
            RewardSpawner.isAttackCAC = false;
            isAttacking = false;
        }
    }


    private void SendBackProjectile(Collider2D[] hits)
    {
        foreach(Collider2D hit in hits)
        {

            if (hit.gameObject.CompareTag("EnemyProjectil"))
            {
                Projectile enemyProj = hit.GetComponent<Projectile>();
                enemyProj.gameObject.layer = 6;
                enemyProj.isConverted = true;
                enemyProj.dir = dir;
            }
        }
    }
}
