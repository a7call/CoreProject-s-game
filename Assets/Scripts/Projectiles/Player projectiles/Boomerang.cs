using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Projectiles
{ 
    
    private float distance;
    [SerializeField]
    private float backDistance;
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

    private void Launch()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
    private void Update()
    {
       distance = Vector3.Distance(transform.position, playerTransform.position);
       Launch();
       if(distance > backDistance) isComingBack = true;
       if (isComingBack) dir = (playerTransform.position - transform.position).normalized;
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
  

}
