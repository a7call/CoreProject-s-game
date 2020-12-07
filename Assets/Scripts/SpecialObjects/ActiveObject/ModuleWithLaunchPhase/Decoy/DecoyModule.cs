using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyModule : ActiveObjects
{
    [SerializeField] private GameObject decoyObject;

    [SerializeField] private bool isDecoyActivated;

    private Player player;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            SpawnBomb();
            UseModule = false;
        }

        if (decoyObject.transform.position != Vector3.zero)
        {
            print("A");
        }
        else
        {
            print("B");
        }

    }

    private void SpawnBomb()
    {
        Instantiate(decoyObject, transform.position, Quaternion.identity);
        //isDecoyActivated = true;
    }
}
