using UnityEngine;


public abstract class EnemyScriptableObject : ScriptableObject
{
    public float attackRange;
    public float attackRadius;
    public LayerMask hitLayers;


    public int maxHealth;
    public Material whiteMat;
    public Material defaultMat;


    public float moveSpeed;
    public float aggroDistance;

}
