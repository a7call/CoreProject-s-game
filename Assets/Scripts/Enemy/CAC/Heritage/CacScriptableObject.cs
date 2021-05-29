using UnityEngine;


/// <summary>
/// Ce ScriptableObject Type1ScriptableObject contient également le EnemyScriptableObject
/// </summary>

[CreateAssetMenu(fileName = "new Cac", menuName = "CacEnemy")]
public class CacScriptableObject : EnemyScriptableObject, IMonsterData
{
    public float attackRadius;
    public LayerMask hitLayers;
    public float timeToSwich;
    public float attackDelay;
    [SerializeField] private int difficulty;
    public int Difficulty
    {
        get
        {
            return difficulty;
        }
    }
}
