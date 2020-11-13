﻿using UnityEngine;

public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private float minimumCoin = 2;
    [SerializeField] private float maximumCoin = 10;
    [SerializeField] private GameObject coin;



    //AvariceModule
    [HideInInspector]
    protected bool isAvariceModuleAlreadyUse = false;
    [HideInInspector]
    public static bool isAvariceModule;
    [HideInInspector]
    public static float AvariceCoinChanceMultiplier;

    //ReapproModule
    [HideInInspector]
    protected bool isReapproModuleAlreadyUse = false;
    [HideInInspector]
    public static bool isReapproModule;
    [HideInInspector]
    public static float ReapproAmmoChanceMultiplier;

    //MoissoneurModule
    [HideInInspector]
    protected bool isMoissoneurDeCrystauxModuleAlreadyUse = false;
    [HideInInspector]
    public static bool isMoissoneurDeCrystauxModule;
    [HideInInspector]
    public static float MoissoneurFullHeartChanceMultiplier;
    [HideInInspector]
    public static float MoissoneurHalfHeartChanceMultiplier;

    //VampirismeModule
    [HideInInspector]
    protected bool isVampirismeModuleUse = false;
    [HideInInspector]
    public static bool isVampirismeModule;
    [HideInInspector]
    public static bool isAttackCAC;
    [HideInInspector]
    public static float VampirismeFullHeartChanceMultiplier;
    [HideInInspector]
    public static float VampirismeHalfHeartChanceMultiplier;

    //VoleurDeTombeModule
    [HideInInspector]
    protected bool isVoleurDeTombeAlreadyUse = false;
    [HideInInspector]
    public static bool isVoleurDeTombeModule;
    [HideInInspector]
    public static float VoleurKeyChanceMultiplier;
    [HideInInspector]
    public static float VoleurCoinChanceMultiplier;



    private float MinimumCoins()
    {
        return this.minimumCoin;
    }
    private float MaximumCoins()
    {
        return this.maximumCoin;
    }

    public void Update()
    {
        if (isAvariceModule && !isAvariceModuleAlreadyUse)
        {
            minimumCoin *= AvariceCoinChanceMultiplier;
            maximumCoin *= AvariceCoinChanceMultiplier;
            isAvariceModuleAlreadyUse = true;
        }

        if (isVoleurDeTombeModule && !isVoleurDeTombeAlreadyUse)
        {
            minimumCoin *= VoleurCoinChanceMultiplier;
            maximumCoin *= VoleurCoinChanceMultiplier;

            chanceToGetKey *= VoleurKeyChanceMultiplier;

            isVoleurDeTombeAlreadyUse = true;
        }

        if (isReapproModule && !isReapproModuleAlreadyUse)
        {
            chanceToGetAmoCase *= ReapproAmmoChanceMultiplier;
            isReapproModuleAlreadyUse = true;
        }

        if(isMoissoneurDeCrystauxModule && !isMoissoneurDeCrystauxModuleAlreadyUse)
        {
            chanceToGetFullHeart *= MoissoneurFullHeartChanceMultiplier;
            chanceToGetHearts *= MoissoneurHalfHeartChanceMultiplier;
            isMoissoneurDeCrystauxModuleAlreadyUse = true;
        }

        
    }

    public void RandomCoinReward(GameObject deadEnemy)
    {
        
        int numberOfCoinToSpawn = (int)Random.Range(this.MinimumCoins(), this.MaximumCoins());
        for(int i=0; i< numberOfCoinToSpawn; i++)
        {
            Instantiate(coin, deadEnemy.transform.position + RandomVector(Vector3.zero, Vector3.one), Quaternion.identity);
        }
    }

    private Vector3 RandomVector( Vector3 min, Vector3 max)
    {
       Vector3 myVector = new Vector3(Random.Range(min.x, max.x),Random.Range(min.y, max.y));
       return myVector;
    }


    [SerializeField] private GameObject key;
    [SerializeField] private float chanceToGetKey;


    private float ChanceToDrop()
    {
        return Random.Range(0f, 1f);
    }

    public void SpawnKeyReward(GameObject deadEnemy)
    {
        if (ChanceToDrop() >= 1 - chanceToGetKey)
        {
            Instantiate(key, deadEnemy.transform.position, Quaternion.identity);
        }
    }


    [SerializeField] private GameObject fullHeart;
    [SerializeField] private GameObject halfHeart;
    [SerializeField] private float chanceToGetHearts;
    [SerializeField] private float chanceToGetFullHeart;


    public void SpawnHeartReward(GameObject deadEnemy)
    {
        //VampirismeModule
        if (isVampirismeModule && !isVampirismeModuleUse && isAttackCAC)
        {
            chanceToGetFullHeart *= VampirismeFullHeartChanceMultiplier;
            chanceToGetHearts *= VampirismeHalfHeartChanceMultiplier;
            isVampirismeModuleUse = true;
        }

        //Debug.Log(chanceToGetFullHeart);

        if (ChanceToDrop() >= 1 - chanceToGetHearts)
        {
            if (ChanceToDrop() >= 1 - chanceToGetFullHeart)
            {
                Instantiate(fullHeart, deadEnemy.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(halfHeart, deadEnemy.transform.position, Quaternion.identity);
            }
           
        }
        //VampirismeModule
        if (isVampirismeModuleUse)
        {
            chanceToGetFullHeart /= VampirismeFullHeartChanceMultiplier;
            chanceToGetHearts /= VampirismeHalfHeartChanceMultiplier;
            isVampirismeModuleUse = false;
        }
    }

    [SerializeField] private GameObject amoCase;
    [SerializeField] private float chanceToGetAmoCase;



    public void SpawnAmoReward(GameObject deadEnemy)
    {
        if (ChanceToDrop() >= 1 - chanceToGetAmoCase)
        {
            Instantiate(amoCase, deadEnemy.transform.position, Quaternion.identity);
        }
    }

}
