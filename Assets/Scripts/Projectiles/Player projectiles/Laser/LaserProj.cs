using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProj : PlayerProjectiles
{
    [SerializeField] float activeTime = 0f;
    [SerializeField] protected LayerMask HitLayer = 0;
    private WeaponsManagerSelected weaponManager;

    protected  void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponentInChildren<WeaponsManagerSelected>();
    }



    protected override void Update()
    {
        if (weaponManager.GetComponentInChildren<DistanceWeapon>().OkToShoot)
        {
            dir = (weaponAttackP.attackPoint.position - playerTransform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(weaponAttackP.attackPoint.position, dir, Mathf.Infinity, HitLayer);

            Debug.DrawRay(weaponAttackP.attackPoint.position, dir * 20, Color.red);
            if (hit.collider != null)
            {

                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(weaponDamage);
            }


        }
    }

}
