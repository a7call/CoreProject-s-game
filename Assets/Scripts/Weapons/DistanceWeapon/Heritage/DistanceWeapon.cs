using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWeapon : Weapons
{
    [SerializeField] protected DistanceWeaponScriptableObject DistanceWeaponData;
    protected GameObject projectile;
    protected override void Awake()
    {
        base.Awake();
        SetData();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetAttackDirection();
        if (Input.GetKeyDown(KeyCode.L))
        {

        }
        
    }

   protected void Shoot()
    {
        Instantiate(projectile, attackPoint.position, Quaternion.identity);
    }

    private void SetData()
    {
        projectile = DistanceWeaponData.projectile;
        enemyLayer = DistanceWeaponData.enemyLayer;
        damage = DistanceWeaponData.damage;
        attackDelay = DistanceWeaponData.AttackDelay;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, 0);
        Gizmos.color = Color.red;
    }
}
