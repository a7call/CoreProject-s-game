using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonZeus : DistanceWeapon
{

    [SerializeField] protected float radius;

    [SerializeField] protected LayerMask hitLayer;
    [SerializeField] protected float knockBackforce;
    [SerializeField] protected float knockBackTime;
    private Vector2 dirTir;

    protected RaycastHit2D[] hits;

    protected override void SetData()
    {
        enemyLayer = DistanceWeaponData.enemyLayer;
        damage = DistanceWeaponData.damage;
        attackDelay = DistanceWeaponData.delayBetweenAttack;
        Dispersion = DistanceWeaponData.Dispersion;
        MagSize = DistanceWeaponData.MagSize;
        ReloadDelay = DistanceWeaponData.ReloadDelay;

        BulletInMag = MagSize;
        image = DistanceWeaponData.image;
    }

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
                    Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                    CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(knockBackforce, dirTir, knockBackTime, enemyScript));
                    enemyScript.TakeDamage(damage);
                    
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
