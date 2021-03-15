using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe mère des Distance et héritière de Enemy.cs
/// Elle contient une fonction setData permettant de récupérer les données du scriptable object 
/// Une fonction aggro permettant de commencer à suivre l'ennemi si le joueur est à porté
/// Une fonction permettant de savoir si le joueur est en range de shoot
/// Une coroutine de shoot
/// Une fonction de shoot qui instansiate le projectile
/// </summary>
public class Distance : Enemy
{
    // Scriptable Object
    [SerializeField] protected DistanceScriptableObject DistanceData;
    // Check si tire
    [HideInInspector]
    public bool isShooting;
    [HideInInspector]
    public float Dispersion;
    Transform randomTargetPointTransform;
    Vector3 randomPoint;

    protected override void Awake()
    {
        base.Awake();
       
    }
    protected override void Update()
    {
        randomTargetPointTransform.position = target.position + randomPoint;
        base.Update();
    }
    protected override void GetReference()
    {
        base.GetReference();
        GameObject randomTargetPoint = new GameObject();
        randomTargetPoint.name = "targetMouvePoint";
        randomTargetPoint.transform.parent = gameObject.transform;
        randomTargetPointTransform = randomTargetPoint.transform;
        randomPoint = (Vector3)Random.insideUnitCircle;
        targetSetter.target = randomTargetPointTransform;
    }
    protected virtual void SetData()
    {
        maxHealth = DistanceData.maxHealth;
        whiteMat = DistanceData.whiteMat;
        defaultMat = DistanceData.defaultMat;
        aIPath.repathRate = Random.Range(DistanceData.refreshPathTime, DistanceData.refreshPathTime2);
        aIPath.pickNextWaypointDist = Random.Range(DistanceData.nextWayPoint, DistanceData.nextWayPoint2);
        aIPath.maxSpeed = Random.Range(DistanceData.moveSpeed, DistanceData.moveSpeed2);
        restTime = DistanceData.restTime;
        projetile = DistanceData.projetile;
        attackRange = Random.Range(DistanceData.attackRange, DistanceData.attackRange2);
        timeToSwitch = DistanceData.timeToSwich;
        Dispersion = DistanceData.Dispersion;
        EnemyPoint = DistanceData.enemyPoints;
    }

    protected override void isInRange()
    {
            if (Vector3.Distance(transform.position, target.position) < attackRange  )
            {
                currentState = State.Attacking;
                isShooting = true;
                isReadyToSwitchState = false;
                aIPath.canMove = false;
            }
            else
            {
                if (currentState == State.Attacking && !isInTransition ) StartCoroutine(transiChasing());
                if (isReadyToSwitchState && currentState != State.Chasing)
                {
                    currentState = State.Chasing;
                    isShooting = false;
                }

            }
       
    }

    
    // Check si prêt à tirer
    protected bool isReadytoShoot = true;
    // Repos après tire
    protected float restTime;
    // Projectile to instantiate
    protected GameObject projetile;
    protected virtual IEnumerator CanShoot()
    {
        if (isShooting && isreadyToAttack)
        {
            isreadyToAttack = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isreadyToAttack = true;
        }
    }
    // Instansiate projectiles
    protected virtual void Shoot()
    {
        float decalage = Random.Range(-Dispersion, Dispersion);
        Transform supportArme = transform.Find("SupportArme");
        Transform weapon = supportArme.Find("Weapon");
        Transform attackPoint = weapon.Find("attackPoint");
        if (attackPoint!= null)
        {
            GameObject myProjectile = Instantiate(projetile, attackPoint.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
            Projectile ScriptProj = myProjectile.GetComponent<Projectile>();
            ScriptProj.Dispersion = decalage;
        }
        else
        {
            GameObject myProjectile = Instantiate(projetile, transform.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
            Projectile ScriptProj = myProjectile.GetComponent<Projectile>();
            ScriptProj.Dispersion = decalage;
        }
      

    }


}
