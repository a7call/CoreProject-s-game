using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.Utils;

public class Spawner
{
    private List<GameObject> _monsters;
    public Spawner(List<GameObject> monsters)
    {
        _monsters = monsters;
    }

    public GameObject Spawn(GameObject monster, Vector3 pos, Transform parent)
    {
        var ret = GameObject.Instantiate(monster, pos, Quaternion.identity);
        ret.transform.parent = parent;
        return ret;
    }

    
}
