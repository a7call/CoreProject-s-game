using System.Collections;
using UnityEngine;

public class ModuleInertie : PassiveObjects
{
    public static PlayerEnergy player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerEnergy>();
    }
    void Start()
    {
        player.maxStacks++;
        player.currentStack = player.maxStacks;
    }
}
