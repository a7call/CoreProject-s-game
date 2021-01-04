using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAstronautEggPopMother : Distance
{
    [SerializeField] private GameObject parasiteRampant;
    // Retirer le serializeField
    [SerializeField] private float radius = 1f;

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

    private void MobPosition()
    {
        for (int i = 0; i < listOfParasite.Length; i++)
        {
            Vector2 transf2D = new Vector2(transform.position.x, transform.position.y);
            listOfParasite[i] = transf2D + radius * Random.insideUnitCircle.normalized;
            print(" Parasite numéro " + i + " est à la position " + listOfParasite[i]);
        }
    }

    //private void CheckPosition()
    //{
    //    float securityCoeff = 0.2f;
    //    MobPosition();
    //    int index = 1;
    //    if(Vector2.Distance(listOfParasite[index],listOfParasite[index+1])<securityCoeff)
    //}

    // Fonction pour instantier les ennemis lorsqu'il meurt
    public override void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        StartCoroutine(WhiteFlash());
        if (currentHealth < 1)
        {
            print("Position du mob avant destruction " + transform.position);
            //PositionToInstantiate();
            parasiteRampant.SetActive(true);
            for (int i = 0; i < listOfParasite.Length; i++)
            {
                Vector2 transf2D = new Vector2(transform.position.x, transform.position.y);
                listOfParasite[i] = transf2D + radius*Random.insideUnitCircle.normalized;
                Instantiate(parasiteRampant, listOfParasite[i], Quaternion.identity);
                print(" Parasite numéro " + i + " est à la position " + listOfParasite[i]);
            }
            SpawnRewards();
        }
    }

}
