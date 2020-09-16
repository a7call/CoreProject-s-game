using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeCacMouvement : Type1
{
    private SpeCacAttack SpeCacAttack;



    private void Start()
    {
        SpeCacAttack = GetComponent<SpeCacAttack>();
        SetData();
    }
    private void Update()
    {
        Aggro();
    }


    // Aggro si pas entrain de charger
    protected override void Aggro()
    {
        if (!SpeCacAttack.isCharging)
        {
            Vector3 dir = (targetToFollow.position - transform.position).normalized;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            return;
        }
    }


    protected override void Patrol()
    {
        
    }
    protected override void SetData()
    {
        base.SetData();
    }

   
}
