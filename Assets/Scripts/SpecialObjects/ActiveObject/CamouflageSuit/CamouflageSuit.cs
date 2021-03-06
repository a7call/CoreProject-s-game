﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamouflageSuit : CdObjects
{
    [SerializeField] private float camouflageTime;
    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            StartCoroutine(Camouflage());
            UseModule = false;
        }
    }

    private IEnumerator Camouflage()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
           
            enemy.isreadyToAttack = false;
            enemy.StopAllCoroutines();
            

        }     
        yield return new WaitForSeconds(camouflageTime);
        foreach (Enemy enemy in enemies)
        {
            enemy.isreadyToAttack = true;
        }
    }
}
