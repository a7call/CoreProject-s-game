using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ModuleProtectionRobotique : PassiveObjects
{
    [SerializeField] private GameObject robotsProtection = null;
    [SerializeField] private float spawnTimer = 0f;
    void Start()
    {
        StartCoroutine(SpawnCo());
    }


    private IEnumerator SpawnCo()
    {
        Instantiate(robotsProtection, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnTimer);
        Instantiate(robotsProtection, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnTimer);
        Instantiate(robotsProtection, transform.position, Quaternion.identity);
    }
}
