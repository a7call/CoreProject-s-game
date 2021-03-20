using UnityEngine;


[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
   
    public int maxHealth;
    public Material whiteMat;
    public Material defaultMat;


    public float moveSpeed;
    public float attackRange;
    public float nextWayPoint;
    public float refreshPathTime;


}
