using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private Collider2D[] hits;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private float radiusSpeed;
    [SerializeField] private LayerMask hitLayer;
    

    private void Update()
    {
        GetAllEnemies();
    }

    private void GetAllEnemies()
    {
        hits = Physics2D.OverlapCircleAll(transform.position,range, hitLayer);

        foreach(Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>().currentState = Enemy.State.KnockedBack;
                Transform hitTrans = hit.GetComponent<Transform>();
                Vector2 dir = (hitTrans.position - transform.position).normalized;
                hitTrans.Translate(-dir * radiusSpeed * Time.deltaTime, Space.World); ;
                hit.transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

