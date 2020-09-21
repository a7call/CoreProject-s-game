using UnityEngine;

[CreateAssetMenu(fileName = "new Type1", menuName = "Type1Enemy")]
public class Type1ScriptableObject : EnemyScriptableObject
{
    public float attackRange;
    public float attackRadius;
    public LayerMask hitLayers;
}
