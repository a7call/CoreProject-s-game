using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoRobotModule : ActiveObjects
{

    public static List<Enemy> enemiesTouched = new List<Enemy>();
    [SerializeField] private float damage = 0f;
    private void Start()
    {
        PlayerProjectiles.isNanoRobotModule = true;
    }
    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            ActivateNanoRobot();
            UseModule = false;
        }
    }

    private void ActivateNanoRobot()
    {
        foreach(Enemy enemy in enemiesTouched)
        {
            if (enemy == null) continue;
            enemy.TakeDamage(damage);   
        }
        enemiesTouched.Clear();
    }

}
