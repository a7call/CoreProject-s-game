using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceWeapon : Weapons
{
    [SerializeField] protected DistanceWeaponScriptableObject DistanceWeaponData;
    protected GameObject projectile;
    protected PlayerProjectiles Proj;
    protected float Dispersion;
    protected bool IsMagEmpty;
    protected int BulletInMag;
    protected float ReloadDelay;
    protected int MagSize;
    [SerializeField] protected Text AmmoText;
    [SerializeField] protected bool InfiniteAmmo;

    protected override void Awake()
    {
        base.Awake();
        SetData();
        Proj = projectile.GetComponent<PlayerProjectiles>();
        AmmoText.gameObject.SetActive(true);
        
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

        DisplayAmmo();
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
        BulletInMag = 0;
        IsMagEmpty = true;
        yield return new WaitForSeconds(ReloadDelay);
        BulletInMag = MagSize;
        IsMagEmpty = false;
    }

    protected void DisplayAmmo()
    {
        if (!InfiniteAmmo)
        {
            AmmoText.text = BulletInMag.ToString();
        }

        else
        {
            AmmoText.text = "Infini";
        }
    }
}
