using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Enemy
{
    // Liste des ennemies que le healer peut heal
    [SerializeField] protected List<GameObject> ennemies;
    // Montant du heal;
    protected int amountToheal;
    // Boolean permettant de savoir si le healer est en range de heal
    private bool isInSight = false;
    // Boolean permettant de savoir si à/doit commencer à heal
    public bool startHealing = false;
    // Boolean permettant de fermer la bouble de GetEnnemitoHeal()
    public bool hasFinished = true;
    //  count du nombre d'ennemi n'ayant pas subis de dégats
    int count = 0;
    // Ennemi entrain de se faire heal
    GameObject EnnemiHealed;
    //Distance à laquel le healer peut soigner;
    [SerializeField] float healingDistance;
    // couroutine déclaration
    Coroutine mycouroutine;
    // Cd de heal 
   [SerializeField] protected float healCd;

    private void Start()
    {
        // Set the state to patrol
        currentState = State.Patrolling;
        // Initialise la liste
        List<GameObject> ennemies = new List<GameObject>();
        // Set point to lui même car pas de patrouille
        targetPoint = transform;

    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Attacking :
                // voir détails
                GetEnnemiToHeal();
                // vérifi si l'énnemis en cours de heal est tué, si c'est le cas stop la couroutine de heal et recommence une boucle
                checkEnnemiDestroyed(EnnemiHealed);
                // vérifi si l'énnemi à heal est à distance 
                checkehealingDistance();
                break;
            case State.Chasing:
                // algo de Pathfinding
                MoveToPath();
                // voir détails
                GetEnnemiToHeal();
                // vérifi si l'énnemis en cours de heal est tué, si c'est le cas stop la couroutine de heal et recommence une boucle
                checkEnnemiDestroyed(EnnemiHealed);
                // vérifi si l'énnemi à heal est à distance 
                checkehealingDistance();
                break;
            case State.Patrolling:
                // voir détails
                GetEnnemiToHeal();
                break;
        }
       
    }



    private void GetEnnemiToHeal()
    {
        // Lance 1 seule boucle. Pour lancer une boucle hasFinished n'est remis à true que une fois l'action en cours terminé. 
        if (hasFinished)
        {
        hasFinished = false;
        
        // à modifier, il faut prendre une valeur de base ...
        int lowerEnnemiHealth = 4;
        // Cast List to Array pour manipulation
        GameObject[] ennemiArray = ennemies.ToArray();

            // Loop sur les ennemis de la liste
            for (int i = 0; i < ennemiArray.Length; i++)
            {

                // Cleaning de l'array après destruction d'un ennemi
                if (ennemiArray[i] == null)
                {
                    ennemies.Remove(ennemiArray[i]);
                    hasFinished = true;
                    targetPoint = transform;
                    return;
                }


                // Récupère un ennemi à heal si il à perdu de la vie
                CheckEnneiesHp(ennemiArray,  i);

                // Verifie qu'un ennemi ait perdu des hp
                if (ennemiArray[i].GetComponent<Enemy>().currentHealth < lowerEnnemiHealth && startHealing)
                {
                    // Stock cette ennemi
                    EnnemiHealed = ennemiArray[i];

                    // Si à porté..
                    if (isInSight)
                    {
                        // S'arrete
                        rb.velocity = Vector2.zero;
                        // Passe le state à attack
                        currentState = State.Attacking;
                        // ??
                        lowerEnnemiHealth = ennemiArray[i].GetComponent<Enemy>().currentHealth;
                        // lance la couroutine de heal
                        mycouroutine = StartCoroutine(HealEnnemiCo(EnnemiHealed, 1));
                    }
                    else
                    {
                        // reboucle 
                        hasFinished = true;
                        // Passe l'etat à chase
                        currentState = State.Chasing;
                        // change target vers l'ennemi à heal
                        targetPoint = ennemiArray[i].GetComponent<Transform>().transform;
                    }

                }
            }
        }
       
    }



    private IEnumerator HealEnnemiCo(GameObject _ennemiToHeal, int _amountToHeal)
    {
        while (_ennemiToHeal.GetComponent<Enemy>().currentHealth < _ennemiToHeal.GetComponent<Enemy>().maxHealth && isInSight )
        {
            hasFinished = false;
            _ennemiToHeal.GetComponent<Enemy>().currentHealth += _amountToHeal;
            yield return new WaitForSeconds(healCd);
        }
        hasFinished = true;
        yield break;
    }


    private void checkEnnemiDestroyed(GameObject _ennemiToHeal)
    {
        if (_ennemiToHeal == null && startHealing && isInSight)
        {
            currentState = State.Patrolling;
            hasFinished = true;
            StopCoroutine(mycouroutine);
            targetPoint = transform;
        }
        else return;
    }


    private void  checkehealingDistance()
    {
       if(EnnemiHealed != null)
        {
            if (Vector2.Distance(transform.position, EnnemiHealed.GetComponent<Transform>().transform.position) < healingDistance)
            {
                isInSight = true;
            }
            else
            {
                isInSight = false;
            }
        }
        
    }



    private void CheckEnneiesHp(GameObject[] _ennemiArray, int i)
    {
        
        if (_ennemiArray[i].GetComponent<Enemy>().currentHealth < _ennemiArray[i].GetComponent<Enemy>().maxHealth)
        {
            startHealing = true;
            count = 0;
        }
        else
        {

            count++;

            if (count == _ennemiArray.Length)
            {
                count = 0;
                startHealing = false;
                hasFinished = true;
            }
        }
    }


}
