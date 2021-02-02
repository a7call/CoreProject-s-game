using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergieProjectile : PlayerProjectiles
{
    [SerializeField] protected float ExplosionDelay;
    [SerializeField] protected float ExplosionRadius;
    [SerializeField] protected float ExploDamage;
    [SerializeField] protected LayerMask hit;

    protected override void Launch()
    {
        base.Launch();
        StartCoroutine(ExplosionDelayCo());
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(weaponDamage);
            CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, dir, knockBackTime, enemy));
            //Modules
            ModuleProcs(enemy);
            Explosion();
        }
        
        else if (collision.CompareTag("WallHautBas"))
        {
            directionTir = new Vector3(directionTir.x, -directionTir.y, directionTir.z);
            transform.Translate(directionTir * speed * Time.deltaTime, Space.World);
        }

        else if (collision.CompareTag("WallLateral"))
        {
            directionTir = new Vector3(-directionTir.x, directionTir.y, directionTir.z);
            transform.Translate(directionTir * speed * Time.deltaTime, Space.World);
        }

    }
    protected override void ModuleProcs(Enemy enemy)
    {
        //if (isExplosiveAmo)
        //{
        //    ExplosiveAmoModule.explosionFnc(this.gameObject);
        //}
        if (isNanoRobotModule)
        {
            NanoRobotModule.enemiesTouched.Add(enemy);
        }
        if (isImolationModule)
        {
            CoroutineManager.Instance.StartCoroutine(ImmolationModule.ImolationDotCo(enemy));
        }
        if (isCryoModule)
        {
            CoroutineManager.Instance.StartCoroutine(CryogenisationModule.CryoCo(enemy));
        }
        if (isParaModule)
        {
            CoroutineManager.Instance.StartCoroutine(ParalysieModule.ParaCo(enemy));
        }
    }

    protected IEnumerator ExplosionDelayCo()
    {
        yield return new WaitForSeconds(ExplosionDelay);
        Explosion();

    }

    protected void Explosion()
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, hit);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                ExplosionEffects(enemy);
            }
            //if (hit.gameObject.GetComponent<PlayerHealth>())
            //{
            //    PlayerHealth player = hit.gameObject.GetComponent<PlayerHealth>();
            //    player.TakeDamage(1);
            //}

        }

        Destroy(gameObject);
    }
    

    protected virtual void ExplosionEffects(Enemy enemy)
    {
        Vector3 Direction = (enemy.transform.position - gameObject.transform.position).normalized;
        CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, Direction, knockBackTime, enemy));
        if (PlayerProjectiles.isNuclearExplosionModule)
        {
            CoroutineManager.Instance.StartCoroutine(NuclearExplosionModule.NuclearDotCo(enemy));
        }
        enemy.TakeDamage(ExploDamage);

        
    }
}
