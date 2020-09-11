using UnityEngine;


[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public float attackRange;
    public float attackRadius;
    public LayerMask player;
    public LayerMask enemy;


    public int maxHealth;
    public Material whiteMat;
    public Material defaultMat;


    public float moveSpeed;
    public float aggroDistance;

}
