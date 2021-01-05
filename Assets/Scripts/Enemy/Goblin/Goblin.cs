using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    [SerializeField] protected EnemyScriptableObject enemyData;

    private void Start()
    {
        currentState = State.Patrolling;
        SetData();
        SetMaxHealth();
    }

    private void SetData()
    {
        moveSpeed = enemyData.moveSpeed;

        maxHealth = enemyData.maxHealth;
        whiteMat = enemyData.whiteMat;
        defaultMat = enemyData.defaultMat;
    }

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Patrolling:
                Test();
                break;
        }
    }

    private void Test()
    {
        print("Hello");
    }

}
