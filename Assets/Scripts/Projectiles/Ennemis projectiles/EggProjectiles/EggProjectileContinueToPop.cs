using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggProjectileContinueToPop : Enemy
{
    [SerializeField] protected GameObject mobs;
    [SerializeField] protected float spawnCd;


    protected override void Awake()
    {
        healthBarGFX.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        StartCoroutine(PopMobs());
    }
    protected IEnumerator PopMobs()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCd);
            Instantiate(mobs, transform.position, Quaternion.identity);
        }
    }

}
