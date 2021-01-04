using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionMine : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float ExplosionRadius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private LayerMask hit;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;
    protected bool isActive = false;
    protected float timeBeforActive = 1.5f;

    private void Start()
    {
        Invoke("Activation", timeBeforActive);
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>());
        CircleCollider2D CirlceCollider = gameObject.AddComponent<CircleCollider2D>();
        CirlceCollider.radius = radius;
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Explosion();
        }
    }
    private void Explosion()
    {
        if (isActive)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, hit);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.GetComponent<Enemy>())
                {
                    Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                    Vector3 Direction = (enemy.transform.position - gameObject.transform.position).normalized;
                    CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, Direction, knockBackTime, enemy));
                    enemy.TakeDamage(explosionDamage);
                    
                }

                if (hit.gameObject.GetComponent<PlayerHealth>())
                {
                    PlayerHealth player = hit.gameObject.GetComponent<PlayerHealth>();
                    player.TakeDamage(1);
                }
                Destroy(gameObject);
            }
        } 
    }

    private void Activation()
    {
        isActive = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
