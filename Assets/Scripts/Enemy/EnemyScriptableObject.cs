using UnityEngine;

public abstract class EnemyScriptableObject : ScriptableObject
{
   
    public int maxHealth;
    public float damage;
    public LayerMask hitLayer;
    public float moveForce;
    public float attackRange;
    public float knockBackForceToApply;
    public float restTime;
    public float InSight;
}
