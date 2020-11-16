using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleAssitanceRobotique : PassiveObjects
{
    [SerializeField] private GameObject robotsAssitant = null;
    void Start()
    {
        Instantiate(robotsAssitant, transform.position, Quaternion.identity);
    }

}
