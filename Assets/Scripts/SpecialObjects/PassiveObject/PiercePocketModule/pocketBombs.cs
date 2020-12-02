using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocketBombs : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private float timeBeforActivation;
    [SerializeField] private LayerMask hit;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;

    private void Start()
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(timeBeforActivation);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hit);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                print(enemy);
                Vector3 Direction = (enemy.transform.position - gameObject.transform.position).normalized;
                CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, Direction, knockBackTime, enemy));
                enemy.TakeDamage(explosionDamage);
                
            }
           
        }
        Destroy(gameObject);
    }
}
