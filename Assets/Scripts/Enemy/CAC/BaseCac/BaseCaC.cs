using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCaC : Cac
{

    
    private void Start()
    {
        
        FindPlayer();
        targetPoint = target;
        SetData();
        SetMaxHealth();
    }


    protected void Update()
    {
        Aggro();
        isInRange();
        GetPlayerPos();
        if(!isInAttackRange)MoveToPath();
    }

    // Find player to follow
    private void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //Mouvement
    protected override void Aggro()
    {
            isPatroling = false;
            targetPoint = target;
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
