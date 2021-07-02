using System.Collections;
using UnityEngine;

public class ModuleInertie : PassiveObjects
{
    public static Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    void Start()
    {

    }
}
