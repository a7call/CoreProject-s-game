﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalysieModule : PassiveObjects
{
    [SerializeField] public static float ParaTimer;
    
    [SerializeField] private float ParaT;

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
            float baseMoveSpeed = enemy.moveSpeed;
            isParaActive = true;
            enemy.moveSpeed = 0;
            yield return new WaitForSeconds(ParaTimer);
            isParaActive = false;
            enemy.moveSpeed = baseMoveSpeed;
        }
        
    }
}
