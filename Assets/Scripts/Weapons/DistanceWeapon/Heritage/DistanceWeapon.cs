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
           StartCoroutine(Shoot());
        }
        
    }

   protected IEnumerator Shoot()
    {
        if(!isAttacking)
        {
            isAttacking = true;
            Instantiate(projectile, attackPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }
       
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
        Gizmos.DrawWireSphere(attackPoint.position, 0.4f);
        Gizmos.color = Color.red;
    }
}
