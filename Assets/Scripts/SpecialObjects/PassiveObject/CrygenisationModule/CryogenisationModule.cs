using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryogenisationModule : PassiveObjects
{
    [SerializeField] public static float cryoTimer;
    [SerializeField] public static float slowMutliplier;
    [SerializeField] private float cryoT = 0f;
    [SerializeField] private float slowM = 0f;
    private void Awake()
    {
        cryoTimer = cryoT;
        slowMutliplier = slowM;
    }
    private void Start()
    {
        PlayerProjectiles.isCryoModule = true;
    }

    public static IEnumerator CryoCo(Enemy enemy)
    {
        if (!enemy.IsSlowed)
        {
            enemy.IsSlowed = true;
            float baseMoveSpeed = enemy.aIPath.maxSpeed;
            enemy.aIPath.maxSpeed *= slowMutliplier;
            yield return new WaitForSeconds(cryoTimer);
            enemy.aIPath.maxSpeed = baseMoveSpeed;
            enemy.IsSlowed = false;
        }
            
    }
}
