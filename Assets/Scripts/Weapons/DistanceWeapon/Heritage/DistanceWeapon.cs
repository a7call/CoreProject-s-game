using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWeapon : Weapons
{
    [SerializeField] protected DistanceWeaponScriptableObject DistanceWeaponData;
    protected GameObject projectile;
    protected PlayerProjectiles Proj;
    protected float Dispersion;
    protected bool IsMagEmpty;
    public int BulletInMag;
    protected float ReloadDelay;
    protected int MagSize;

    protected override void Awake()
    {
        base.Awake();
        SetData();
        Proj = projectile.GetComponent<PlayerProjectiles>();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isTotalDestructionModule && !damagealReadyMult)
        {
            damagealReadyMult = true;
            damage *= damageMultiplier;
        }
        GetAttackDirection();
        if (Input.GetMouseButton(0))
        {
           StartCoroutine(Shoot());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
        
    }

   protected virtual IEnumerator Shoot()
    {
        if(!isAttacking && !IsMagEmpty)
        {
            float decalage = Random.Range(-Dispersion, Dispersion);
            isAttacking = true;
            Proj.Dispersion = decalage;
            BulletInMag--;
            Instantiate(projectile, attackPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
            if (BulletInMag <= 0)
            {
                IsMagEmpty = true;
                StartCoroutine(Reload());
            }
        }
       
    }

    protected virtual void SetData()
    {
        projectile = DistanceWeaponData.projectile;
        enemyLayer = DistanceWeaponData.enemyLayer;
        damage = DistanceWeaponData.damage;
        attackDelay = DistanceWeaponData.AttackDelay;
        Dispersion = DistanceWeaponData.Dispersion;
        MagSize = DistanceWeaponData.MagSize;
        ReloadDelay = DistanceWeaponData.ReloadDelay;

        BulletInMag = MagSize;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, 0.4f);
        Gizmos.color = Color.red;
    }

    protected IEnumerator Reload()
    {
        IsMagEmpty = true;
        yield return new WaitForSeconds(ReloadDelay);
        BulletInMag = MagSize;
        IsMagEmpty = false;
    }
}
