using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProj : PlayerProjectiles
{
    [SerializeField] float activeTime = 0f;
    [SerializeField] protected LayerMask HitLayer = 0;
    //protected Enemy enemy;
    

    protected override void Update()
    {
        StartCoroutine(destroy());

        dir = (weaponAttackP.attackPoint.position - playerTransform.position).normalized;

        StartCoroutine(destroy());
            RaycastHit2D hit = Physics2D.Raycast(weaponAttackP.attackPoint.position, dir, Mathf.Infinity, HitLayer);

            Debug.DrawRay(weaponAttackP.attackPoint.position, dir * 10, Color.red);
            if (hit.collider != null)
            {
                
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(weaponDamage);
            }
        
    }

    protected IEnumerator destroy()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(gameObject);
    }

}
