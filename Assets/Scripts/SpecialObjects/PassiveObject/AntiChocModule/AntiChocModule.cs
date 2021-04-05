using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiChocModule : PassiveObjects
{

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().currentArmor += 2;
    }
}
