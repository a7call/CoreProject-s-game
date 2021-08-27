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

    protected override void SetData()
    {
        // ScriptableObject Datas
        MaxHealth = DistanceData.maxHealth;
        knockBackForceToApply = DistanceData.knockBackForceToApply;
        restTime = DistanceData.restTime;
        projetile = DistanceData.projetile;
        attackRange = DistanceData.attackRange;
        dispersion = DistanceData.dispersion;
        inSight = DistanceData.InSight;
        difficulty = DistanceData.Difficulty;

        //Chiffre arbitraire à modifier
        var stopAttackingRangeCoef = Utils.RandomizeParams(1.2f, 1.5f);
        stopAttackRange = attackRange * stopAttackingRangeCoef;

        AIMouvement.MoveForce = DistanceData.moveForce;
    }

    public abstract IEnumerator CanShootCO();
    public abstract IEnumerator InstantiateProjectileCO();
}
