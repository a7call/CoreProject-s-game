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
        currentState = State.Chasing;
        // Set data
        SetData();
        SetMaxHealth();
    }

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                isInRange();
                break;
            case State.Attacking:
                isInRange();
                StartCoroutine(CanShoot());
                break;
        }
    }

    // Fonction pour instantier les ennemis lorsqu'il meurt
    public override void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        StartCoroutine(WhiteFlash());
        if (currentHealth < 1)
        {
            foreach (GameObject parasite in listParasite)
            {
                Vector2 transf2D = new Vector2(transform.position.x, transform.position.y);
                Instantiate(parasite, transf2D + radius * Random.insideUnitCircle.normalized, Quaternion.identity);
            }
            SpawnRewards();
        }
    }

}
