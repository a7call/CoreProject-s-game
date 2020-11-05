using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSaber : CacWeapons
{
    private Collider2D coll;
    [SerializeField] private float deflectTime;
    [SerializeField] private float deflectCD;
    private bool readyToDeflect = true;
    
    private void Start()
    {
        coll = gameObject.GetComponent<Collider2D>();
    }
    protected override void Update()
    {
        base.Update();
        if( Input.GetKeyDown(KeyCode.P) && readyToDeflect)
        {
            StartCoroutine(DeflectProjectils());
        }
    }


    private IEnumerator DeflectProjectils()
    {
        readyToDeflect = false;
        readyToAttack = false;
        coll.enabled = true;
        yield return new WaitForSeconds(deflectTime);
        coll.enabled = false;
        readyToAttack = true;
        yield return new WaitForSeconds(deflectCD);
        readyToDeflect = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectil"))
        {
            Projectile projectils = collision.GetComponent<Projectile>();
            projectils.dir = -projectils.dir;
        }
        
    }



    protected override IEnumerator Attack()
    {
        return base.Attack();
    }
}
