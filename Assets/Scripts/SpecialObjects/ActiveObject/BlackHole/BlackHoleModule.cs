using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleModule : StacksObjects
{
    [SerializeField] private GameObject blackHoleModule;

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
        Instantiate(blackHoleModule, transform.position, Quaternion.identity);
    }
}
