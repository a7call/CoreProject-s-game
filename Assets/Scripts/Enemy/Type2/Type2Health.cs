using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2Health : EnemyHealth
{
    [SerializeField] protected Type2ScriptableObject Type2Data;
    private void Start()
    {
        SetData();
    }
    protected void SetData()
    {
        maxHealth = Type2Data.maxHealth;
        whiteMat = Type2Data.whiteMat;
        defaultMat = Type2Data.defaultMat;
    }
}
