using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalysieModule : PassiveObjects
{
    [SerializeField] public static float ParaTimer;
    
    [SerializeField] private float ParaT = 0f;

    [SerializeField] public bool isParalysieActive = false;

    [HideInInspector]
    [SerializeField] public static bool isParaActive;

    private void Awake()
    {
        ParaTimer = ParaT;
        isParaActive = isParalysieActive;
    }
    private void Start()
    {
        PlayerProjectiles.isParaModule = true;
    }

    public static IEnumerator ParaCo(Enemy enemy)
    {
        if (!isParaActive)
        {
            float baseMoveSpeed = enemy.AIMouvement.MoveForce;
            isParaActive = true;
            enemy.AIMouvement.MoveForce = 0;
            yield return new WaitForSeconds(ParaTimer);
            isParaActive = false;
            enemy.AIMouvement.MoveForce = baseMoveSpeed;
        }
        
    }
}
