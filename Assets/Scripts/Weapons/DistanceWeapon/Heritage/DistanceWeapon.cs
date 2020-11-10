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
    protected bool IsReloading;
    protected int BulletInMag;
    protected float ReloadDelay;
    protected int MagSize;
    protected Text AmmoText;
    protected Text AmmoStockText;
    [SerializeField] protected bool InfiniteAmmo;
    [SerializeField] protected int AmmoStock;

    //CanonRapideModule
    [HideInInspector]
    protected bool CadenceAlreadyUp = false;
    [HideInInspector]
    public static bool isCanonRapideModule;
    [HideInInspector]
    public static int CadenceMultiplier;

    //PrecisionModule
    [HideInInspector]
    protected bool PrecisionAlreadyUp = false;
    [HideInInspector]
    public static bool isPrecisionModule;
    [HideInInspector]
    public static int PrecisionMultiplier;

    //FastReloadModule
    [HideInInspector]
    protected bool FastReloadAlreadyActive = false;
    [HideInInspector]
    public static bool isFastReloadModule;
    [HideInInspector]
    public static float ReloadSpeedMultiplier;

    //UnlimitedAmmoModule
    [HideInInspector]
    public static bool isUnlimitedAmmoModule;


    protected override void Awake()
    {
        base.Awake();
        SetData();
        AmmoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
        AmmoStockText = GameObject.FindGameObjectWithTag("AmmoStockText").GetComponent<Text>();
        Proj = projectile.GetComponent<PlayerProjectiles>();



    }
    void Start()
    {
        //AmmoText.gameObject.SetActive(true);
        //AmmoStockText.gameObject.SetActive(true);

    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();

        //UnlimitedAmmoModule
        InfiniteAmmo = isUnlimitedAmmoModule;

        GetAttackDirection();
        if (Input.GetMouseButton(0))
        {
           StartCoroutine(Shoot());
        }

        if (Input.GetKeyDown(KeyCode.R) && (AmmoStock != 0 | InfiniteAmmo))
        {
            StartCoroutine(Reload());
        }

       

        DisplayAmmo();

        if (isCanonRapideModule && !CadenceAlreadyUp)
        {
            CadenceAlreadyUp = true;
            attackDelay /= CadenceMultiplier;
        }

        if (isPrecisionModule && !PrecisionAlreadyUp)
        {
            PrecisionAlreadyUp = true;
            Dispersion /= PrecisionMultiplier;
        }
        if (isFastReloadModule && !FastReloadAlreadyActive)
        {
            FastReloadAlreadyActive = true;
            ReloadDelay /= ReloadSpeedMultiplier;
        }

    }

   protected virtual IEnumerator Shoot()
    {
        if(!isAttacking && BulletInMag > 0 && !IsReloading)
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
        IsReloading = true;
        yield return new WaitForSeconds(ReloadDelay);
        if (AmmoStock + BulletInMag >= MagSize && !InfiniteAmmo)
        {
            AmmoStock = AmmoStock + BulletInMag;
            AmmoStock = AmmoStock - MagSize;
            BulletInMag = MagSize;   
        }
        else if(AmmoStock + BulletInMag <= MagSize && !InfiniteAmmo)
        {
            AmmoStock = AmmoStock + BulletInMag;
            BulletInMag = AmmoStock;
            AmmoStock = AmmoStock - BulletInMag;
        }
        else if (isUnlimitedAmmoModule)
        {
            BulletInMag = MagSize;
        }

        IsReloading = false;
    }

    protected void DisplayAmmo()
    {
        if (!InfiniteAmmo)
        {
            AmmoText.text = BulletInMag.ToString();
            AmmoStockText.text = AmmoStock.ToString();
        }

        else
        {
            AmmoText.text = BulletInMag.ToString();
            AmmoStockText.text = "Infini";
        }
    }
}
