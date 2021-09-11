using System.Collections;
using UnityEngine;
using Wanderer.Utils;

public abstract class Distance : Enemy, IMonster
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
    protected float Dispersion { get; private set; }
    // attackPoint : where projectile should start
    protected Transform attackPoint;
    // stopAttackRange : range ou l'ennemi passe en mode chaising  != attackRange  : range ou l'ennemi passe en mode attaque. 
    protected float StopAttackRange { get; private set; }
    // Check si prêt à tirer
    [SerializeField]
    protected bool isReadytoShoot = true;
    // Repos après tire
    protected float RestTime { get; private set; }
    // Projectile to instantiate
    protected GameObject Projetile { get; private set; } 
    protected float ProjetileSpeed { get; private set; } 

    protected float Damage { get; private set; }

    protected LayerMask HitLayer { get; private set; }

    protected override void SetData()
    {
        // ScriptableObject Datas
        this.MaxHealth = DistanceData.maxHealth;
        this.knockBackForceToApply = DistanceData.knockBackForceToApply;
        this.RestTime = DistanceData.restTime;
        this.Projetile = DistanceData.projetile;
        this.attackRange = DistanceData.attackRange;
        this.Dispersion = DistanceData.dispersion;
        this.inSight = DistanceData.InSight;
        this.Damage = DistanceData.damage;
        this.HitLayer = DistanceData.hitLayer;
        this.ProjetileSpeed = DistanceData.projectileSpeed;
        this.Difficulty = DistanceData.Difficulty;

        //Chiffre arbitraire à modifier
        var stopAttackingRangeCoef = Utils.RandomizeParams(1.2f, 1.5f);
        StopAttackRange = attackRange * stopAttackingRangeCoef;

        AIMouvement.MoveForce = DistanceData.moveForce;
        PoolManager.GetInstance().CreatePool(Projetile, 50);
    }

    public abstract IEnumerator StartShootingProcessCo();
    public abstract IEnumerator InstantiateProjectileCO();
}
