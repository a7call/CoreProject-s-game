using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistorsionTemporelleEnemy : CdObjects
{
    private int roomRange = 50;
    [SerializeField] private LayerMask hitLayer;

    [SerializeField] private float timeDistorsionTemporelle = 10f;

    private List<GameObject> projectilsEnemy = new List<GameObject>();

    private GameObject[] enemies;

    protected override void Start()
    {
        base.Start();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    protected override void Update()
    {
        
        if (UseModule)
        {
            UseModule = false;
            StartCoroutine(DistorsionTemporelleMoveSpeed());
            DistorsionTemporelleProjectile();

        }
    }

    private void DistorsionTemporelleProjectile()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, roomRange, hitLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("EnemyProjectil"))
            {

                if (!projectilsEnemy.Contains(hit.gameObject))
                {
                    projectilsEnemy.Add(hit.gameObject);
                    hit.GetComponent<Projectile>().projectileSpeed /= 2;
                }
            }
        }
    }

    private IEnumerator DistorsionTemporelleMoveSpeed() { 

        foreach (GameObject enemy in enemies)
        {
            if(enemy == null)
            {
                continue;
            }
            enemy.gameObject.GetComponent<Enemy>().AIMouvement.MoveForce /= 2;
        }

        yield return new WaitForSeconds(timeDistorsionTemporelle);

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                continue;
            }
            enemy.gameObject.GetComponent<Enemy>().AIMouvement.MoveForce *= 2;
        }

        foreach (GameObject projectil in projectilsEnemy)
        {
            if(projectil == null)
            {
                continue;
            }
            projectil.GetComponent<Projectile>().projectileSpeed *= 2;
        }


    }

}
