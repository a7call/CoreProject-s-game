using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pompe : Weapons
{
    [SerializeField] protected DistanceWeaponScriptableObject DistanceWeaponData;
    [SerializeField] GameObject[] projectiles;
    [SerializeField] int angleTir;
    public AngleProjectile angleProjectile;

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
        if (!isAttacking)
        {
            isAttacking = true;
            float decalage = angleTir / (projectiles.Length - 1);
            angleProjectile.angleDecalage = -decalage * (projectiles.Length + 1) / 2;

            //base.Shoot();
            for (int i = 0; i < projectiles.Length; i++)
            {
                angleProjectile.angleDecalage = angleProjectile.angleDecalage + decalage;
                GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }

    }

    private void SetData()
    {
        //projectile = DistanceWeaponData.projectile;
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
