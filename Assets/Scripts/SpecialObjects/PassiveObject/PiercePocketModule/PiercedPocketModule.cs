﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercedPocketModule : PassiveObjects
{
    public  GameObject piercedPocketBombs;
    public  float timeBetweenBombs;
    public  bool bombsReady = true;


    public  IEnumerator SpawnBombs()
    {
        bombsReady = false;
        Instantiate(piercedPocketBombs, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(timeBetweenBombs);
        bombsReady = true;
    }
}
