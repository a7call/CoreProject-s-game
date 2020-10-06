using UnityEngine;


public abstract class EnemyScriptableObject : ScriptableObject
{
   
    public int maxHealth;
    public Material whiteMat;
    public Material defaultMat;


    public float moveSpeed;
    public float aggroDistance;
    public float attackRange;

}
