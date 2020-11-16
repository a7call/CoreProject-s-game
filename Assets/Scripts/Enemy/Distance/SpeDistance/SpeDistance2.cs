using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fonction qui sur la première attaque applique un dot
/// Ce dot inflige dammage toutes les deux secondes pendant 10 secondes
/// </summary>

public class SpeDistance2 : Distance
{
    [SerializeField] private bool isFirstAttack = true; // je sais pas c'est quoi ?
    [SerializeField] private float timeDot = 0f;
    [SerializeField] private int dotNumber = 5;
    [SerializeField] private int dotDamages = 20;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        currentState = State.Patrolling;
        // Set data
        SetFirstPatrolPoint();
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
                // suit le path créé et s'arrête pour tirer
                MoveToPath();
                break;
            case State.Attacking:
                isInRange();
                // Couroutine gérant les shoots 
                StartCoroutine("CanShoot");
                StartCoroutine(DotAttack(dotDamages));
                break;
        }
    }

    //Mouvement

    // Override(Enemy.cs) Aggro s'arrete pour tirer et suit le player si plus à distance
    protected override void Aggro()
    {
        targetPoint = target;
    }


    //Health
    private IEnumerator DotAttack(int _dotDamages)
    {
        for (int i = 0; i <= dotNumber; i++)
        {
            playerHealth.currentHealth -= _dotDamages;
            yield return new WaitForSeconds(timeDot);
        }
        isFirstAttack = false;
    }
    
}
