using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPiege : HeritagePiege
{
    #region Variables
    
    
    [SerializeField] private float enemyDamage;
    private bool isExploded =false;

    [SerializeField] private float explosionRadius;
    [SerializeField] private float knockBackForce;
    [SerializeField] private float knockBackTime;

    [SerializeField] protected float screenShakePower = 0.3f;
    [SerializeField] protected float screenShakeDuration = 0.2f;

    

    #endregion Variables


    #region Fonctionnement
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 )
        {
            TriggerPiege();
        }
    }

    private void TriggerPiege()
    {
        if (!isExploded)
        {
            animator.SetTrigger("IsTrigger");
            isExploded = true;
        }
        
    }

    private void FonctionnementPiege()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        CameraController.instance.StartShakeG(screenShakeDuration, screenShakePower);
        Explosion(enemyDamage, hit);
        Collider2D collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;

    }

    private void Explosion(float damage, Collider2D[] Hit)
    {

        foreach (Collider2D hit in Hit)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                Enemy enemyScript = hit.GetComponent<Enemy>();
                enemyScript.TakeDamage(damage);
            }

            if (hit.gameObject.CompareTag("EnemyProjectil")) Destroy(hit.gameObject);

            if (hit.gameObject.CompareTag("Player"))
            {
                player.TakeDamage(1);            }

            if (hit.gameObject.CompareTag("PiegeExplosion"))
            {
                ExplosionPiege explosionPiege = hit.GetComponent<ExplosionPiege>();
                explosionPiege.TriggerPiege();
            }

        }

    }
    #endregion Fonctionnement
}
