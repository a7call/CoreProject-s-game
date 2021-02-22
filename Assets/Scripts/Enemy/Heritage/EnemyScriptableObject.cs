using UnityEngine;


[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
   
    public int maxHealth;
    public Material whiteMat;
    public Material defaultMat;


    public float moveSpeed;
    public float moveSpeed2;
    public float attackRange;
    public float attackRange2;

}
