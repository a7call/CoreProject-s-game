using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sur cet ennemi, pensez à mettre un projectile classique avec une animation qui ressemble à de la salive

public class TentacleAstronautEggPopMother : Distance
{
    [SerializeField] private GameObject parasiteRampant;

    // Retirer le serializeField
    [SerializeField] private float radius = 2f;

    [SerializeField] private Vector2[] listOfParasite = new Vector2[3]; 

    void Start()
    {
        currentState = State.Patrolling;
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
            for (int i = 0; i < listOfParasite.Length; i++)
            {
                Vector2 transf2D = new Vector2(transform.position.x, transform.position.y);
                listOfParasite[i] = transf2D + radius*Random.insideUnitCircle.normalized;
                Instantiate(parasiteRampant, listOfParasite[i], Quaternion.identity);
            }
            SpawnRewards();
        }
    }

}
