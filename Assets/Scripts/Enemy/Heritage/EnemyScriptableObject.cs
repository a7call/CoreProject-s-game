using UnityEngine;

public abstract class EnemyScriptableObject : ScriptableObject
{
   
    public int maxHealth;
    public float moveForce;
    public float attackRange;
    public float knockBackForceToApply;
    public float nextWayPoint;
    public float refreshPathTime;
    public float restTime;
    public float InSight;
    public bool isSupposedToMoveAttacking = true;

}
