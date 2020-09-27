using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Enemy
{
    public List<GameObject> ennemies;
    protected int amountToheal;
    protected bool isInSight = false;
    public bool startHealing = false;
    public bool hasFinished = true;
    int count = 0;
    GameObject nouveauCandidat;
    GameObject EnnemiHealed;
    [SerializeField] float healingDistance;
    Coroutine mycouroutine;
   [SerializeField] protected float healCd;

    private void Start()
    {
        currentState = State.Patrolling;
        List<GameObject> ennemies = new List<GameObject>();
        targetPoint = transform;

    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Attacking :
                GetEnnemiToHeal();
                checkEnnemiDestroyed(EnnemiHealed);
                checkehealingDistance();
                break;
            case State.Chasing:
                MoveToPath();
                GetEnnemiToHeal();
                checkEnnemiDestroyed(EnnemiHealed);
                checkehealingDistance();
                break;
            case State.Patrolling:
                GetEnnemiToHeal();
                checkEnnemiDestroyed(EnnemiHealed);
                break;
        }
       
    }



    private void GetEnnemiToHeal()
    {
        if (hasFinished)
        {
        hasFinished = false;

        int lowerEnnemiHealth = 4;
        GameObject[] ennemiArray = ennemies.ToArray();


            for (int i = 0; i < ennemiArray.Length; i++)
            {
                if (ennemiArray[i] == null)
                {
                    ennemies.Remove(ennemiArray[i]);
                    hasFinished = true;
                    targetPoint = transform;
                    return;
                }


                nouveauCandidat = ennemiArray[i];

                CheckEnneiesHp(ennemiArray,  i);

                if (nouveauCandidat.GetComponent<Enemy>().currentHealth < lowerEnnemiHealth && startHealing)
                {
                    EnnemiHealed = nouveauCandidat;


                    if (isInSight)
                    {
                        print(nouveauCandidat.GetComponent<Enemy>().currentHealth);
                        rb.velocity = Vector2.zero;
                        currentState = State.Attacking;
                        lowerEnnemiHealth = nouveauCandidat.GetComponent<Enemy>().currentHealth;
                        mycouroutine = StartCoroutine(HealEnnemiCo(EnnemiHealed, 1));
                    }
                    else
                    {
                        hasFinished = true;
                        currentState = State.Chasing;
                        targetPoint = nouveauCandidat.GetComponent<Transform>().transform;
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
