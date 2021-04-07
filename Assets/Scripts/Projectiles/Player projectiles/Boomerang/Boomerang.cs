using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : PlayerProjectiles
{ 
    
    private float distance;
    [SerializeField]
    private float backDistance = 0f;
    private bool isComingBack;
    public static bool isAlreadyFired;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        DestroyIfnotBack();
        isAlreadyFired = true;
    }

    
    protected override void Update()
    {
        
       //distance = Vector3.Distance(transform.position, playerTransform.position);
       Launch();
       if(distance > backDistance) isComingBack = true;
      // if (isComingBack) dir = (playerTransform.position - transform.position).normalized;
        if (distance < 0.2f)
        {
            isAlreadyFired = false;
            Destroy(gameObject);
        }
             
    }

    private void DestroyIfnotBack()
    {
        if (isAlreadyFired) Destroy(gameObject);
    }


    protected override void Launch()
    {
        base.Launch();
    }

}
