using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptionProjectile : SingleProjectile
{
    public GameObject explosionEffect;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        if (collision.gameObject.layer == (int)Mathf.Log(HitLayer.value, 2))
        {           
            var monster = collision.GetComponent<Enemy>();
            monster.animator.SetTrigger(EnemyConst.INTERRUPTION_ANIMATION_NAME);            
            monster.SetState(new InterruptedState(monster));
        }
        base.OnTriggerEnter2D(collision);
    }

    private void OnDisable()
    {
       PoolManager.GetInstance().ReuseObject(explosionEffect, transform.position, transform.rotation);
    }
}
