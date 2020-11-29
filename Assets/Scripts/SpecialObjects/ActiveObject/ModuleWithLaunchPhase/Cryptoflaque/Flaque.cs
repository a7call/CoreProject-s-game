using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flaque : ModuleLauchPhase
{
    private bool isActive;
    [SerializeField] protected float ZoneRadius;
    [SerializeField] protected float ZoneDamage;
    [SerializeField] protected float hitTimer;
    [SerializeField] protected float zoneTimer;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected virtual IEnumerator ZoneCo()
    {
        while (isActive)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, ZoneRadius);

            foreach (Collider2D enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    Enemy enemyScript = enemy.GetComponent<Enemy>();
                    enemyScript.TakeDamage(ZoneDamage);
                }

            }

        yield return new WaitForSeconds(hitTimer);
        }

    }

    protected override void Update()
    {
        base.Update();
        if (isNotMoving && !isAlreadyActive)
        {
            isAlreadyActive = true;
            StartCoroutine(ActiveZone());
            StartCoroutine(ZoneCo());
        }
    }

    private IEnumerator ActiveZone()
    {
        isActive = true;
        yield return new WaitForSeconds(zoneTimer);
        isActive = false;
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ZoneRadius);
    }
}
