using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] private float knockBackForce;
    [SerializeField] private float knockBackTime;
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(1);
            Enemy enemy = gameObject.transform.parent.GetComponent<Enemy>();
            //CoroutineManager.GetInstance().StartCoroutine(enemy.KnockCo(knockBackForce, -enemy.AIMouvement.desiredVelocity.normalized, knockBackTime, enemy));
        } 
    }
}
