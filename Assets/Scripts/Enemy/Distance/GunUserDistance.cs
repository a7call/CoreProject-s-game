using System.Collections;
using UnityEngine;
using Wanderer.Utils;


public abstract class GunUserDistance : Distance
{
    protected EnemyWeaponManager WeaponManager { get; private set; }
    protected Animator WeaponAnimator { get; set; }

    protected override void Start()
    {
        base.Start();
        GetWeaponReferences();
    }
    void GetWeaponReferences()
    {
        WeaponManager = GetComponentInChildren<EnemyWeaponManager>();
        WeaponAnimator = WeaponManager.Weapon.animator;
        attackPoint = WeaponManager.Weapon.attackPoint;
        Utils.AddAnimationEvent("Attack", "CanShootCO", WeaponAnimator);
    }
    public override IEnumerator CanShootCO()
    {
        if (isReadytoShoot)
        {
            isReadytoShoot = false;
            // Wait for coroutine shoot to end
            yield return StartCoroutine(InstantiateProjectileCO());

            isAttacking = false;

            WeaponAnimator.SetBool(EnemyConst.ATTACK_BOOL_CONST, false);
            // delay before next Shoot
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
            // gestion de l'animation d'attaque
            attackAnimationPlaying = false;
        }
    }
    public override IEnumerator InstantiateProjectileCO()
    {
        float decalage = Random.Range(-dispersion, dispersion);
        GameObject myProjectile = Instantiate(projetile, WeaponManager.Weapon.attackPoint.position, Quaternion.identity);
        Projectile ScriptProj = myProjectile.GetComponent<Projectile>();
        ScriptProj.dispersion = decalage;
        myProjectile.GetComponent<Projectile>().SetMoveDirection(WeaponManager.aimDirection);
        yield return new WaitForEndOfFrame();
    }
}

