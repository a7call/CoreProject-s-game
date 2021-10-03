using System.Collections;
using UnityEngine;
using Wanderer.Utils;

public abstract class Distance : Enemy
{
    public override IMonsterData GetMonsterData()
    {
        return DistanceData;
    }
    // Scriptable Object
    [Header("Data")]
    [SerializeField] protected DistanceScriptableObject DistanceData;
    [HideInInspector]
    protected float Dispersion { get; private set; }
    // attackPoint : where projectile should start
    protected Transform attackPoint;
    // stopAttackRange : range ou l'ennemi passe en mode chaising  != attackRange  : range ou l'ennemi passe en mode attaque. 
    protected float StopAttackRange { get; private set; }
    // Repos après tire
    protected float RestTime { get; private set; }
    // Projectile to instantiate
    protected GameObject Projectile { get; private set; } 
    protected float ProjectileSpeed { get; private set; } 

    protected float Damage { get; private set; }

    protected LayerMask HitLayer { get; private set; }

    protected override void SetData()
    {
        // ScriptableObject Datas
        this.MaxHealth = DistanceData.maxHealth;
        this.knockBackForceToApply = DistanceData.knockBackForceToApply;
        this.RestTime = DistanceData.RestTime;
        this.Projectile = DistanceData.projetile;
        this.attackRange = DistanceData.attackRange;
        this.Dispersion = DistanceData.dispersion;
        this.inSight = DistanceData.InSight;
        this.Damage = DistanceData.damage;
        this.HitLayer = DistanceData.hitLayer;
        this.ProjectileSpeed = DistanceData.projectileSpeed;
        this.Difficulty = DistanceData.Difficulty;

        //Chiffre arbitraire à modifier
        var stopAttackingRangeCoef = Utils.RandomizeParams(1.2f, 1.5f);
        StopAttackRange = attackRange * stopAttackingRangeCoef;

        AIMouvement.MoveForce = DistanceData.moveForce;
        PoolManager.GetInstance().CreatePool(Projectile, 100);
    }

    public abstract IEnumerator StartShootingProcessCo();
    public abstract IEnumerator InstantiateProjectileCO();
}
