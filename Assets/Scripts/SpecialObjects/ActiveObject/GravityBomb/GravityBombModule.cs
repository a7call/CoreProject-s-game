using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBombModule : ActiveObjects
{
    [SerializeField] private GameObject gravityBomb;
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
        Instantiate(gravityBomb, transform.position, Quaternion.identity);
    }
}
