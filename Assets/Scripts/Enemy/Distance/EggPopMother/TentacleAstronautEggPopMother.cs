using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sur cet ennemi, penser à mettre un projectile classique avec une animation qui ressemble à de la salive

public class TentacleAstronautEggPopMother : Distance
{
    // Retirer le serializeField
    [SerializeField] private float radius = 2f;

    [HideInInspector]
    [SerializeField] private GameObject[] listParasite = new GameObject[3];

    void Start()
    {
     
        // Set data
        SetData();
        SetMaxHealth();
    }

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Chasing:
                isInRange();
                break;
            case State.Attacking:
                isInRange();
                StartCoroutine(CanShoot());
                break;
        }
    }
    protected override void EnemyDie()
    {
        if (isDying)
        {
            SpawnRewards();
            foreach (GameObject parasite in listParasite)
            {
                Vector2 transf2D = new Vector2(transform.position.x, transform.position.y);
                Instantiate(parasite, transf2D + radius * Random.insideUnitCircle.normalized, Quaternion.identity);
            }
            nanoRobot();
            Destroy(gameObject);
        }
    }

}
