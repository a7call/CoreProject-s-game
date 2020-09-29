using UnityEngine;


/// <summary>
/// Ce ScriptableObject Type1ScriptableObject contient également le EnemyScriptableObject
/// </summary>

[CreateAssetMenu(fileName = "new Type1", menuName = "Type1Enemy")]
public class Type1ScriptableObject : EnemyScriptableObject
{
    public float attackRange;
    public float attackRadius;
    public LayerMask hitLayers;
}
