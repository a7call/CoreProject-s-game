using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeCacMouvement : Type1
{
    private SpeCacAttack SpeCacAttack;
    public GameObject mobs;
    private bool spawned = false;



    private void Start()
    {
        SpeCacAttack = GetComponent<SpeCacAttack>();
        SetData();
        SetFirstPatrolPoint();
    }
    private void Update()
    {
        Patrol();
        Aggro();
    }


    // Aggro si pas entrain de charger
    protected override void Aggro()
    {

        Vector3 dir = (targetToFollow.position - transform.position).normalized;
      
        if (!SpeCacAttack.isCharging && Vector3.Distance(transform.position, targetToFollow.position) < aggroDistance)
        {
            if (!spawned)
            {
                spawned = true;
                GameObject.Instantiate(mobs, transform.position, Quaternion.identity);
            }
            
            isPatroling = false;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
        }
    }


    protected override void Patrol()
    {
        base.Patrol();
    }
    protected override void SetData()
    {
        base.SetData();
    }
    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }


}
