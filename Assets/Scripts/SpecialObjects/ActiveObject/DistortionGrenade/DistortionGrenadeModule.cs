using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionGrenadeModule : ActiveObjects
{
    [SerializeField] private GameObject distortionGrenade;
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
        Instantiate(distortionGrenade, transform.position, Quaternion.identity);
    }
}
