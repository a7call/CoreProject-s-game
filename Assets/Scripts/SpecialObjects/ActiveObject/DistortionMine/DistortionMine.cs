using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionMine : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private LayerMask hit;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;

    private void Start()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Explosion();
        }
        
    }
    private void Explosion()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hit);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                print(enemy);
                CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, RandomDir(), knockBackTime, enemy));
                enemy.TakeDamage(explosionDamage);
                Destroy(gameObject);
            }
        }
        
    }

    private Vector3 RandomDir()
    {
        int choice = Mathf.FloorToInt(Random.value * 3.99f);
        //use that int to chose a direction
        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            default:
                return Vector2.right;
        }
    }
}
