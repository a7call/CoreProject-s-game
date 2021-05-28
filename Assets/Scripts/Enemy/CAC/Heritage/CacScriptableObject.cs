using UnityEngine;


/// <summary>
/// Ce ScriptableObject Type1ScriptableObject contient également le EnemyScriptableObject
/// </summary>

[CreateAssetMenu(fileName = "new Cac", menuName = "CacEnemy")]
public class CacScriptableObject : EnemyScriptableObject, ICharacter
{
    public float attackRadius;
    public LayerMask hitLayers;
    public float timeToSwich;
    public float attackDelay;

    public float AttackRadius { get; set; } = 5f;
}
