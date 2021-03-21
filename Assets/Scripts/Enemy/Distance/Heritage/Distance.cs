using System.Collections;
using System.Threading.Tasks;

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
    public bool isShooting = false; 
    [HideInInspector]
    public float Dispersion;
    Transform randomTargetPointTransform;
    Vector3 randomPoint;
    protected Transform attackPoint;
    protected float attackModeRange;
    protected float coefAttackModeRange;

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Update()
    {
        
        base.Update();
    }
    protected override void GetReference()
    {
        base.GetReference();
        CreatEnemyTargetGo();
        GetAttackPointGO();
    }


    protected void CreatEnemyTargetGo()
    {
        GameObject randomTargetPoint = new GameObject();
        randomTargetPoint.name = "targetMouvePoint";
        randomTargetPoint.transform.parent = target.transform.GetChild(target.transform.childCount-1);
        randomTargetPointTransform = randomTargetPoint.transform;
        randomPoint = (Vector3)Random.insideUnitCircle;
        randomTargetPointTransform.position = target.position + randomPoint;
        targetSetter.target = randomTargetPointTransform;
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

    protected virtual void SetData()
    {
        // ScriptableObject Datas
        maxHealth = DistanceData.maxHealth;
        whiteMat = DistanceData.whiteMat;
        defaultMat = DistanceData.defaultMat;
        restTime = DistanceData.restTime;
        projetile = DistanceData.projetile;
        attackRange = Random.Range(DistanceData.attackRange, DistanceData.attackRange + 2);
        timeToSwitch = DistanceData.timeToSwich;
        Dispersion = DistanceData.Dispersion;


        //Chiffre arbitraire à modifier 
        coefAttackModeRange = Random.Range(1.2f, 1.5f);
        attackModeRange = attackRange * coefAttackModeRange;


        // PathFinding Variable
        aIPath.endReachedDistance = attackRange *2/3;
        aIPath.slowdownDistance = aIPath.endReachedDistance + 0.5f;
        aIPath.repathRate = Random.Range(DistanceData.refreshPathTime, DistanceData.refreshPathTime + 0.5f);
        aIPath.pickNextWaypointDist = Random.Range(DistanceData.nextWayPoint, DistanceData.nextWayPoint + 1f);
        aIPath.maxSpeed = Random.Range(DistanceData.moveSpeed, DistanceData.moveSpeed + 1);
    }

    protected override void isInRange()
    {
       
            if (Vector3.Distance(transform.position, target.position) < attackRange)
            {
            currentState = State.Attacking;
            }
            else if(currentState != State.Chasing && !isShooting && (Vector3.Distance(transform.position, target.position) > attackModeRange))
            {
            currentState = State.Chasing;

            }
       
    }

   protected void ShouldNotMoveDuringShooting()
    {
        if (currentState == State.Chasing && !aIPath.canMove)
        {
            aIPath.canMove = true;
        }else if(currentState == State.Attacking && aIPath.canMove)
        {
            aIPath.canMove = false;
        }
    }

    
    // Check si prêt à tirer
    [SerializeField]
    protected bool isReadytoShoot = true;
    // Repos après tire
    protected float restTime;
    protected int restTime2 = 3000;
    // Projectile to instantiate
    protected GameObject projetile;
    protected virtual IEnumerator CanShoot()
    {
        if (isReadytoShoot)
        {
            isReadytoShoot = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }
    }
    protected virtual IEnumerator CanShootCO()
    {
        if (isReadytoShoot)
        {
            isReadytoShoot = false;
            yield return StartCoroutine(ShootCO());
            yield return new WaitForSeconds(3f);
            isReadytoShoot = true;
        }
    }


    // Instansiate projectiles
    protected virtual void Shoot()
    {
        float decalage = Random.Range(-Dispersion, Dispersion);
            
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
    protected virtual IEnumerator ShootCO()
    {

        
        float decalage = Random.Range(-Dispersion, Dispersion);

        if (attackPoint != null)
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
        yield return new WaitForEndOfFrame();
    }


}
