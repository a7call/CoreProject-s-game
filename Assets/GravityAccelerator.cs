using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAccelerator : ActiveObjects
{
    [SerializeField] private GameObject acceleratorBomb;
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
        Instantiate(acceleratorBomb, transform.position, Quaternion.identity);
    }
}
