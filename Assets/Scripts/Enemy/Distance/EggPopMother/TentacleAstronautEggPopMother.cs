using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sur cet ennemi, penser à mettre un projectile classique avec une animation qui ressemble à de la salive

public class TentacleAstronautEggPopMother : DistanceWithWeapon
{
    private float radius = 2f;
    [SerializeField] private List<GameObject> enemyToPop = new List<GameObject>();
    [SerializeField] private int numberToPop = 3;

    protected  void DestroyEnemy()
    {
        float angle = 0;
        if (isDying)
        {
            Vector3 firstSpawn = transform.position + radius * (Vector3)Random.insideUnitCircle.normalized;
            for (int i=0; i < numberToPop; i++)
            {
                Vector3 spawnPos = transform.position + (Quaternion.AngleAxis(angle, Vector3.forward) * firstSpawn).normalized;
                angle += 360 / numberToPop;
                Instantiate(enemyToPop[Random.Range(0, enemyToPop.Count)], spawnPos, Quaternion.identity);
            }
        }
    }

}
