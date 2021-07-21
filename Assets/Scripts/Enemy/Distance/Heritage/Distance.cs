using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.Utils;
/// <summary>
/// Classe mère des Distance et héritière de Enemy.cs
/// Elle contient une fonction setData permettant de récupérer les données du scriptable object 
/// Une coroutine de shoot
/// Une fonction de shoot qui instansiate le projectile
/// </summary>
public class Distance : Enemy, IMonster
{
    public IMonsterData Datas
    {
        get
        {
            return DistanceData;
        }
    }
    // Scriptable Object
    [SerializeField] protected DistanceScriptableObject DistanceData;
    [HideInInspector]
    public float dispersion;
    // attackPoint : where projectile should start
    protected Transform attackPoint;
    // stopAttackRange : range ou l'ennemi passe en mode chaising  != attackRange  : range ou l'ennemi passe en mode attaque. 
    protected float stopAttackRange;
 // Check si prêt à tirer
    [SerializeField]
    protected bool isReadytoShoot = true;
    // Repos après tire
    protected float restTime;
    // Projectile to instantiate
    protected GameObject projetile;

    protected override void GetReference()
    {
        base.GetReference();
        GetAttackPointGO();
    }
    void GetAttackPointGO()
    {
        if (transform.Find("SupportArme"))
        {
            Transform supportArme = transform.Find("SupportArme");
            Transform weapon = supportArme.Find("Weapon");
            attackPoint = weapon.Find("attackPoint");
        }
        else
        {
            Debug.LogWarning("It seems that this monster lack of a weapon");
        }
    }

  
    protected override void SetData()
    {
        // ScriptableObject Datas
        MaxHealth = DistanceData.maxHealth;
        restTime = DistanceData.restTime;
        projetile = DistanceData.projetile;
        attackRange = Random.Range(DistanceData.attackRange, DistanceData.attackRange + Utils.RandomizeParams(-1, 2));
        dispersion = DistanceData.dispersion;
        inSight = DistanceData.InSight;
        isSupposedToMoveAttacking = DistanceData.isSupposedToMoveAttacking;
        difficulty = DistanceData.Difficulty;

        //Chiffre arbitraire à modifier
        var stopAttackingRangeCoef = Utils.RandomizeParams(1.2f, 1.5f);
        stopAttackRange = attackRange * stopAttackingRangeCoef;

        AIMouvement.Speed = Random.Range(DistanceData.moveSpeed, DistanceData.moveSpeed + Utils.RandomizeParams(-0.1f, 0.1f));
    }

    //protected override void isInRange()
    //{

    //        if (Vector3.Distance(transform.position, target.position) < attackRange)
    //        {
    //        currentState = State.Attacking;
    //        }
    //        else if(currentState != State.Chasing && !isAttacking && (Vector3.Distance(transform.position, target.position) > stopAttackRange))
    //        {
    //        currentState = State.Chasing;

    //        }

    //}

    protected virtual void ChangeStateWithRange()
    {
        if (Vector3.Distance(transform.position, target.position) < inSight && currentState == State.Patrolling)
        {
            currentState = State.Chasing;
            AIMouvement.ShouldMove = true;
        }

        if (!AIMouvement.ShouldMove)
            return;

        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
        }
        else if (currentState != State.Chasing && !isAttacking && (Vector3.Distance(transform.position, target.position) > stopAttackRange))
        {
            currentState = State.Chasing;
        }

    }


    // Start Shoot Sequence
    protected virtual IEnumerator CanShootCO()
    {
        if (isReadytoShoot)
        {
            isReadytoShoot = false;
            // Wait for coroutine shoot to end
            yield return StartCoroutine(ShootCO());
            isAttacking = false;
            // delay before next Shoot
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
            // gestion de l'animation d'attaque
            attackAnimationPlaying = false;
            

        }
    }


    // Instansiate projectiles
    protected virtual IEnumerator ShootCO()
    {

        
        float decalage = Random.Range(-dispersion, dispersion);

        if (attackPoint != null)
        {
            GameObject myProjectile = Instantiate(projetile, attackPoint.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
            Projectile ScriptProj = myProjectile.GetComponent<Projectile>();
            ScriptProj.dispersion = decalage;

        }
        else
        {
            GameObject myProjectile = Instantiate(projetile, transform.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
            Projectile ScriptProj = myProjectile.GetComponent<Projectile>();
            ScriptProj.dispersion = decalage;
        }
        yield return new WaitForEndOfFrame();
    }


}
