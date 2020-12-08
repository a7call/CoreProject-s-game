using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBang : ActiveObjects
{
    // Variables pour générer le cercle et voir si y'a des ennemis dans la zone
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private float radiusZone = 5f;
    [SerializeField] private float stunTime = 5f;

    private bool state;

    private List<GameObject> enemiesInRange = new List<GameObject>();

    protected override void Start()
    {

    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && readyToUse)
        {
            StartCoroutine(StateCoroutine());
            UseSunBang();
            StartCoroutine(CdToReUse());
            state = true;
        }

        if (!UseModule && state)
        {
            ResetEnemyState();
        }
    }

    private void UseSunBang()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radiusZone, hitLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                if (!ModuleAlreadyUse)
                {
                    enemiesInRange.Add(hit.gameObject);
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
        foreach (GameObject enemy in enemiesInRange)
        {
            if(enemy == null)
            {
                continue;
            }
            enemy.gameObject.GetComponent<Enemy>().currentState = Enemy.State.Chasing;
        }
        enemiesInRange.Clear();
        ModuleAlreadyUse = false;
        state = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusZone);
    }
}
