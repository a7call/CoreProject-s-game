using UnityEngine;

public abstract class EnemyScriptableObject : ScriptableObject
{
   
    public int maxHealth;
    public float moveSpeed;
    public float attackRange;
    public float nextWayPoint;
    public float refreshPathTime;
    public float InSight;
    public bool isSupposedToMoveAttacking = true;

}
