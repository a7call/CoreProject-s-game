using UnityEngine;

/// <summary>
/// ScriptableObject des Items
/// </summary>
/// 
[CreateAssetMenu(fileName = "Item", menuName = "Iventory/Item")]
public class ItemScriptableObject : ScriptableObject
{
    //Général
    public int id;
    public string nameObject;
    public string description;
    public Sprite Image;
    public int coastAtTrader;

    //Potion d'HP
    public int hpGiven;

    //Potion de MS
    public float projectileSpeed;
    public float speedDuration;

    //Potions d'AS/UpAttack
    public float increasedDamages;
    public float attackSpeed;
    public float attackSpeedDuration;

    //Potions de shield
    public int pointShield;

}
