using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce ScriptableObject Type2ScriptableObject hérite en plus des paramètres du ScriptableObject EnemyScriptableObject
/// </summary>


[CreateAssetMenu(fileName = "new Distance", menuName = "DistanceEnemy")]
public class DistanceScriptableObject : EnemyScriptableObject
{
    public float restTime;
    public GameObject projetile;
    public float timeToSwich;
    public float timeIntervale;
    public int nbTir;
    public float Dispersion;

}
