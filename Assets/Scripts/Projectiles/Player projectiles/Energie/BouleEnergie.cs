using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouleEnergie : PlayerProjectiles
{
    [SerializeField] protected float ExplosionDelay;
    [SerializeField] protected float ExplosionRadius;
    [SerializeField] protected float ExploDamage;
    [SerializeField] protected float Force;
    [SerializeField] protected float KnockBackExploForce;
    [SerializeField] protected float KnockBackExploTime;



    //protected override void Launch()
    //{
    //    var directionTir = Quaternion.AngleAxis(Dispersion, Vector3.forward) * transform.right;
    //    rb.AddForce(directionTir * Force);
    //    StartCoroutine(ExplosionDelayCo());
        
    //}
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage);
            Explosion();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        return; 
    }

    protected IEnumerator ExplosionDelayCo()
    {
        yield return new WaitForSeconds(ExplosionDelay);
        Explosion();
    }

    protected void Explosion()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, WeaponLayer);
        
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                ExplosionEffects(enemy);
            }
        }
        Destroy(gameObject);
    }


    protected void ExplosionEffects(Enemy enemy)
    {
        Vector3 Direction = (enemy.transform.position - gameObject.transform.position).normalized;
        enemy.TakeDamage(ExploDamage);
        

    }
}
