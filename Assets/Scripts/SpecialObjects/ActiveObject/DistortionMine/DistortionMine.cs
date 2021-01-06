using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionMine : ExplosivesModule
{
    [SerializeField] private float ExplosionRadius;

    protected bool isActive = false;
    protected float timeBeforActive = 1.5f;

    protected override void Start()
    {
        base.Start();
        Invoke("Activation", timeBeforActive);
        CircleCollider2D cirlceCollider = gameObject.AddComponent<CircleCollider2D>();
        cirlceCollider.isTrigger = true;
        cirlceCollider.radius = radius;
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>());
    
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isActive)
        {
            StartCoroutine(ExplosionOnEnemy());
        }
    }
    private void Activation()
    {
        isActive = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
