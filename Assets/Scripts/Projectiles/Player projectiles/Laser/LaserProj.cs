using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProj : PlayerProjectiles
{
    [SerializeField] float activeTime;
    [SerializeField] protected LayerMask HitLayer;
    void Update()
    {
        StartCoroutine(destroy());

        
        

            StartCoroutine(destroy());
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, HitLayer);

            Debug.DrawRay(transform.position, dir * 10, Color.red);
            if (hit.collider != null)
            {
                print("test");
            }


        
    }

    protected IEnumerator destroy()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(gameObject);
    }
}
