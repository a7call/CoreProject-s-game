using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bubble360 : Distance
{
    [SerializeField] protected GameObject rayon;

    [SerializeField] private List<GameObject> differentRadius = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        SetData();
        SetMaxHealth();
    }

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
          
            case State.Chasing:
                isInRange();
                break;
            case State.Attacking:
                isInRange();
                StartCoroutine(CanShoot());
                break;
        }
        if (isDying)
        {
           GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(192, 2, 0, 1));
        }
    }


    protected override void SetAnimationVariable()
    {

        if (aIPath.canMove)
        {
            animator.SetFloat("HorizontalSpeed", aIPath.velocity.x);
            animator.SetFloat("VerticalSpeed", aIPath.velocity.y);
            float EnemySpeed = aIPath.velocity.sqrMagnitude;
            animator.SetFloat("Speed", EnemySpeed);
        }
        else
        {

            animator.SetFloat("HorizontalSpeed", 0);
            animator.SetFloat("VerticalSpeed", 0);
            float EnemySpeed = 0;
            animator.SetFloat("Speed", EnemySpeed);
        }

        //mettre d'autres conditions 
        

        if (currentState == State.KnockedBack)
        {
            //animator.SetBool("isTakingDamage", true);
        }
        else
        {
            //animator.SetBool("isTakingDamage", false);
        }
    }

    //Voir Enemy.cs(héritage)
    protected override IEnumerator CanShoot()
    {
        if (isReadytoShoot)
        {
            animator.SetTrigger("isAttacking");
            isReadytoShoot = false;
            Shoot();
            
            yield return new WaitForSeconds(restTime);
           
            isReadytoShoot = true;
        }
    }

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
       
        Instantiate(rayon, transform.position, Quaternion.identity);
        AddShoot();
      
    }

    private void AddShoot()                     
    {
        differentRadius.Insert(0,rayon);
    }
}
