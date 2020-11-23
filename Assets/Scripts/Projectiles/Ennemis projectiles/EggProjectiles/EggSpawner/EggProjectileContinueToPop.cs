using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggProjectileContinueToPop : Enemy
{
    [SerializeField] protected GameObject mobs;
    [SerializeField] protected float spawnCd;
    [SerializeField] protected SpawnerScriptableObject SpawnerData;


    protected override void Awake()
    {
        healthBarGFX.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        SetMaxHealth();
    }
    void Start()
    {
        StartCoroutine(PopMobs());
    }
    protected IEnumerator PopMobs()
    {
        while (true)
        {
            print("test");
            yield return new WaitForSeconds(spawnCd);
            Instantiate(mobs, transform.position, Quaternion.identity);
        }
    }


    private void SetData()
    {
        maxHealth = SpawnerData.maxHealth;
    }

}
