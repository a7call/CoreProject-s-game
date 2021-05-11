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
public class Distance : Enemy
{
    // Scriptable Object
    [SerializeField] protected DistanceScriptableObject DistanceData;
    [HideInInspector]
    public float dispersion;
    // attackPoint : where projectile should start
    protected Transform attackPoint;
    // attackModeRange : range ou l'ennemi passe en mode chaising  != attackRange  : range ou l'ennemi passe en mode attaque. 
    protected float attackModeRange;
    protected float coefAttackModeRange;
 // Check si prêt à tirer
    [SerializeField]
    protected bool isReadytoShoot = true;
    // Repos après tire
    protected float restTime;
    // Projectile to instantiate
    protected GameObject projetile;


    protected override void Update()
    {
        base.Update();
      

        switch (currentState)
        {
            case State.Chasing:
                isInRange();
                break;
            case State.Attacking:
                isInRange();
                StartCoroutine(CanShootCO());
                break;
        }
    }
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

        //Chiffre arbitraire à modifier 
        coefAttackModeRange = Utils.RandomizeParams(1.2f, 1.5f);
        attackModeRange = attackRange * coefAttackModeRange;


        // PathFinding Variable
        aIPath.endReachedDistance = attackRange *2/3;
        aIPath.slowdownDistance = aIPath.endReachedDistance + 0.5f;
        aIPath.repathRate = Random.Range(DistanceData.refreshPathTime, DistanceData.refreshPathTime + Utils.RandomizeParams(-0.5f, 0.5f));
        aIPath.pickNextWaypointDist = Random.Range(DistanceData.nextWayPoint, DistanceData.nextWayPoint + Utils.RandomizeParams(-0.2f, 0.2f));
        aIPath.maxSpeed = Random.Range(DistanceData.moveSpeed, DistanceData.moveSpeed + Utils.RandomizeParams(-0.1f, 0.1f));
    }

    protected override void isInRange()
    {
       
            if (Vector3.Distance(transform.position, target.position) < attackRange)
            {
            currentState = State.Attacking;
            }
            else if(currentState != State.Chasing && !isAttacking && (Vector3.Distance(transform.position, target.position) > attackModeRange))
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
