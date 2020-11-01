using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWeapon : Weapons
{
    [SerializeField] protected DistanceWeaponScriptableObject DistanceWeaponData;
    protected GameObject projectile;
    [SerializeField] protected PlayerProjectiles Proj;
    protected float Dispersion;

    protected override void Awake()
    {
        base.Awake();
        SetData();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        GetAttackDirection();
        if (Input.GetMouseButton(0))
        {
           StartCoroutine(Shoot());
        }
        
    }

   protected virtual IEnumerator Shoot()
    {
        if(!isAttacking)
        {
            float decalage = Random.Range(-Dispersion, Dispersion);
            isAttacking = true;
            Proj.Dispersion = decalage;
            Instantiate(projectile, attackPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }
       
    }

    protected virtual void SetData()
    {
        projectile = DistanceWeaponData.projectile;
        enemyLayer = DistanceWeaponData.enemyLayer;
        damage = DistanceWeaponData.damage;
        attackDelay = DistanceWeaponData.AttackDelay;
        Dispersion = DistanceWeaponData.Dispersion;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, 0.4f);
        Gizmos.color = Color.red;
    }
}
