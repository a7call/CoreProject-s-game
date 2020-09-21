using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCaC : Cac
{


    private void Start()
    {
        FindPlayer();
        SetData();
        SetMaxHealth();
    }


    protected void Update()
    {
        Aggro();
        isInRange();
        GetPlayerPos();
    }

    // Find player to follow
    private void FindPlayer()
    {
        targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //Mouvement
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

   

    protected override void SetData()
    {
        base.SetData();
    }

   
  

    //Health


    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();

    }



    //Attack


    protected override void isInRange()
    {
        base.isInRange();
    }

    protected override void BaseAttack()
    {
        base.BaseAttack();
    }

    protected override void GetPlayerPos()
    {
        base.GetPlayerPos();
    }


}
