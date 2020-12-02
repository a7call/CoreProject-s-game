using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyModule : ActiveObjects
{
    [SerializeField] private GameObject decoyObject;
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
        Instantiate(decoyObject, transform.position, Quaternion.identity);
    }
}
