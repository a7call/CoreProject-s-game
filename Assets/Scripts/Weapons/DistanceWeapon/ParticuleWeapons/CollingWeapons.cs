using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollingWeapons : DistanceWeapon
{
    //[SerializeField] float LoadingDelay;
    protected int count =0;
    protected bool IsToHot = false;
    protected bool IsCooling = false;
    protected float coolingTime = 3f;
    protected float coolingDelay = 0.5f;
    protected int countMax = 6;
    protected float knockBackforce = 15f;
    protected float knockBackTime = 0.1f;
    
}
