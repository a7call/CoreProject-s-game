using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryogenisationModule : PassiveObjects
{
    [SerializeField] public static float cryoTimer;
    [SerializeField] public static float slowMutliplier;
    [SerializeField] private float cryoT;
    [SerializeField] private float slowM;
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
            float baseMoveSpeed = enemy.moveSpeed;
            enemy.moveSpeed *= slowMutliplier;
            yield return new WaitForSeconds(cryoTimer);
            enemy.moveSpeed = baseMoveSpeed;
    }
}
