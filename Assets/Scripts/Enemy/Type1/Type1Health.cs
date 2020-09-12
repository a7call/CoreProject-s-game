using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Health : EnemyHealth
{
    // Rien d'autre pour le moment 
    [SerializeField] protected Type1ScriptableObject Type1Data;

    void Start()
    {
        SetData();
    }

    protected void SetData()
    {
        maxHealth = Type1Data.maxHealth;
        whiteMat = Type1Data.whiteMat;
        defaultMat = Type1Data.defaultMat;
    }
}
