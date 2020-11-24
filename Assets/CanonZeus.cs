using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonZeus : DistanceWeapon
{

    [SerializeField] protected float radius;

    [SerializeField] protected LayerMask hitLayer;
    private Vector2 dirTir;

    protected RaycastHit2D[] hits;

    protected override void Update()
    {
        dirTir = (attackPoint.position - GetComponentInParent<Transform>().position).normalized;
        base.Update();
    }
    protected override IEnumerator Shoot()
    {
        if (!isAttacking && BulletInMag > 0 && !IsReloading)
        {
            isAttacking = true;
            BulletInMag--;
            hits = Physics2D.CircleCastAll(transform.position, radius, dirTir, Mathf.Infinity, hitLayer);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().TakeDamage(damage);
                    continue;
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    Destroy(gameObject);
                }

            }
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
            if (BulletInMag <= 0)
            {
                StartCoroutine(Reload());
            }
        }
    }

    public void OnDrawGizmos()
    {

    }
}
