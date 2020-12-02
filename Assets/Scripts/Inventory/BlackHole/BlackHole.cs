using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private Collider2D[] hits;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private float radiusSpeed;
    [SerializeField] private float timeToExplod;
    [SerializeField] private LayerMask hitLayer;
    List<Collider2D> hitsList = new List<Collider2D>();

    private void Start()
    {
        StartCoroutine(DestroyAll());
    }
    private void Update()
    {
        GetAllEnemies();
        if (hitsList.Count > 1) AttractAllEnemies();
    }

    private void GetAllEnemies()
    {
        hits = Physics2D.OverlapCircleAll(transform.position,range, hitLayer);


        foreach(Collider2D hit in hits)
        {
            if(hit.CompareTag("Enemy")|| hit.CompareTag("EnemyProjectil"))
            {
                hitsList.Add(hit);
            }
            
           
        }
    }

    void AttractAllEnemies()
    {
        foreach(Collider2D hit in hitsList)
        {
            if (hit == null) continue;
            if (hit.CompareTag("Enemy"))
            {
                Transform hitTrans = hit.GetComponent<Transform>();
                Vector2 dir = (hitTrans.position - transform.position).normalized;
                hitTrans.Translate(-dir * radiusSpeed * Time.deltaTime, Space.World); ;
                hit.transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
                hit.GetComponent<Enemy>().currentState = Enemy.State.KnockedBack;
            }
            if (hit.CompareTag("EnemyProjectil"))
            {
                hit.GetComponent<Projectile>().isDisabled = true;
                Transform hitTrans = hit.GetComponent<Transform>();
                Vector2 dir = (hitTrans.position - transform.position).normalized;
                hitTrans.Translate(-dir * radiusSpeed * Time.deltaTime, Space.World); ;
                hit.transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
            }
        }
    }

    private IEnumerator DestroyAll()
    {
        yield return new WaitForSeconds(timeToExplod);
        foreach (Collider2D hit in hitsList)
        {
            Destroy(hit.gameObject);
            
        }
        hitsList.Clear();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

