﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BubbleMS : Distance
{
    [SerializeField] protected GameObject rayon;

    [SerializeField] private List<GameObject> differentRadius = new List<GameObject>();

    public GameObject deathObject;
    
    private bool firstShoot = true;

    protected override void Awake()
    {
        base.Awake();
        SetData();
        SetMaxHealth();
    }

    protected override void Update()
    {
        
        base.Update();
        EnabledRayon();
        switch (currentState)
        {
            case State.Chasing:
                isInRange();
                break;
            case State.Attacking:
                StartCoroutine(CanShoot());
                break;
        }
        ShouldNotMoveDuringShooting();

    }
   
    protected override void EnemyDie()
    {
        if (isDying)
        {
            isDying = false;
            SpawnRewards();
            Instantiate(deathObject, transform.position, Quaternion.identity);
            nanoRobot();
            Destroy(gameObject);
        }
    }

    protected override IEnumerator CanShoot()
    {
        if (isReadytoShoot && firstShoot)
        {
            isReadytoShoot = false;
            firstShoot = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
        }
    }

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        GameObject monRayon = Instantiate(rayon, transform.position, Quaternion.identity);
        monRayon.transform.parent = gameObject.transform;
        AddShoot();
    }

    private void AddShoot()
    {
        differentRadius.Insert(0, rayon);
    }

    private void EnabledRayon()
    {
        if (ParasiteIdol.parasiteIdolFear)
        {
            if(gameObject.transform.GetChild(1) != null) 
            {
                // gameObject.transform.GetChild(1).GetComponent<RadiusGrowUp>().enabled = false;
                GameObject myRayon = GameObject.FindGameObjectWithTag("RayonMS");
                Destroy(myRayon);
            }
            ParasiteIdol.parasiteIdolFear = false;
            firstShoot = true;
            isReadytoShoot = true;
        }
    }

}
