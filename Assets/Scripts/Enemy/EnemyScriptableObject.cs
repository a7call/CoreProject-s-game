using UnityEngine;

public abstract class EnemyScriptableObject : ScriptableObject, IMonsterData
{

    public int maxHealth;
    public float damage;
    public LayerMask hitLayer;
    public float moveForce;
    public float attackRange;
    public float knockBackForceToApply;
    public float InSight;
    [field: SerializeField] public int Difficulty { get; set; }
    [field: SerializeField] public float RestTime { get; set; }
}
