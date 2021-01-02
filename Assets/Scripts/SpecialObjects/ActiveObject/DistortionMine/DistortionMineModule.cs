using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionMineModule : StacksObjects
{
    [SerializeField] private GameObject distortionMine;
    // Start is called before the first frame update

    // Update is called once per frame
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
        Instantiate(distortionMine, transform.position, Quaternion.identity);
    }
}
