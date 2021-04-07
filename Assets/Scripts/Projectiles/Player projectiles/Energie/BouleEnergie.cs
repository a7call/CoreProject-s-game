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
    [SerializeField] protected LayerMask hit;

    [HideInInspector]
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(directionTir * Force);
    }
    protected override void Launch()
    {
        StartCoroutine(ExplosionDelayCo());
        
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, directionTir, knockBackTime, enemy));
            //Modules
            ModuleProcs(enemy);
            Explosion();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
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
            
        }

        Destroy(gameObject);
    }


    protected virtual void ExplosionEffects(Enemy enemy)
    {
        Vector3 Direction = (enemy.transform.position - gameObject.transform.position).normalized;
        CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(KnockBackExploForce, Direction, KnockBackExploTime, enemy));
        if (PlayerProjectiles.isNuclearExplosionModule)
        {
            CoroutineManager.Instance.StartCoroutine(NuclearExplosionModule.NuclearDotCo(enemy));
        }
        enemy.TakeDamage(ExploDamage);
        

    }
}
