using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCacMouvement : Type1
{
    protected override void Aggro()
    {
        Vector3 dir = (targetToFollow.position - transform.position).normalized;
            isPatroling = false;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
    }

    protected override void Patrol()
    {
        base.Patrol();
    }

    protected  void Update()
    {
        Aggro();
    }

    protected override void SetData()
    {
        base.SetData();
    }
    
    private void Start()
    {
        FindPlayer();
        SetData();
    }
    // Find player to follow
    private void FindPlayer()
    {
        targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
