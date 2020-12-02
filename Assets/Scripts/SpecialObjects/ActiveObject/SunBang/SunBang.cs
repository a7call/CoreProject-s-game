using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBang : ActiveObjects
{
    // Variables pour générer le cercle et voir si y'a des ennemis dans la zone
    protected RaycastHit2D[] hits;
    [SerializeField] private float radiusZone = 5f;
    [SerializeField] private float stunTime = 5f;

    private Enemy enemy;

    private GameObject[] enemies;

    protected override void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && readyToUse)
        {
            StartCoroutine(StateCoroutine());
            UseSunBang();
        }

        if (ModuleAlreadyUse)
        {
            ResetEnemyState();
            Destroy(gameObject);
        }
    }
    private void UseSunBang()
    {
        hits = Physics2D.CircleCastAll(transform.position, radiusZone, Vector2.zero, Mathf.Infinity);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                if (!ModuleAlreadyUse)
                {
                    hit.transform.GetComponent<Enemy>().currentState = Enemy.State.Paralysed;
                }

            }
        }
    }

    private IEnumerator StateCoroutine()
    {
        UseModule = true;
        yield return new WaitForSeconds(stunTime);
        UseModule = false;
        readyToUse = false;
        ModuleAlreadyUse = true;
    }

    private void ResetEnemyState()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.gameObject.GetComponent<Enemy>().currentState = Enemy.State.Attacking;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusZone);
    }
}
