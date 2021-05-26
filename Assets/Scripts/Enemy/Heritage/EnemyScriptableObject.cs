using UnityEngine;


[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
   
    public int maxHealth;
   

    public float moveSpeed;
    public float attackRange;
    public float nextWayPoint;
    public float refreshPathTime;
    public float InSight;
    public bool isSupposedToMoveAttacking = true;
    public int difficulty;

}
