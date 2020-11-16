using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fonction qui sur la première attaque applique un dot
/// Ce dot inflige dammage toutes les deux secondes pendant 10 secondes
/// </summary>

public class SpeDistance2 : Distance
{
    [SerializeField] private bool isFirstAttack = true;
    [SerializeField] private float timeDot;
    [SerializeField] private int dotNumber = 5;
    [SerializeField] private int dotDamages = 20;
    
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        currentState = State.Chasing;
        // Set data
        targetPoint = transform;
        SetData();
        SetMaxHealth();
    }

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Chasing:
                // récupération de l'aggro
                Aggro();
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


    protected override void SetData()
    {
        base.SetData();
    }

    //Mouvement

    // Override(Enemy.cs) Aggro s'arrete pour tirer et suit le player si plus à distance
    protected override void Aggro()
    {
        targetPoint = target;
    }

    protected override void PlayerInSight()
    {
        base.PlayerInSight();
    }

    protected override void isInRange()
    {
        base.isInRange();
    }

    

    //Voir Enemy.cs(héritage)
    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
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
    


    // Voir Enemy.cs (héritage)
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }


   

    // Voir Enemy.cs (héritage)
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }

    //Attack


    // Voir Distance.cs (héritage)
    protected override void Shoot()
    {
        base.Shoot();
    }
}
