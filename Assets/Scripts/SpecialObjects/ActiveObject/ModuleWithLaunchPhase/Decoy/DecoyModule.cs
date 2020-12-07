using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyModule : ActiveObjects
{
    [SerializeField] private GameObject decoyObject;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            SpawnBomb();
            UseModule = false;
        }
    }

    private void SpawnBomb()
    {
        Instantiate(decoyObject, transform.position, Quaternion.identity);
    }
}
