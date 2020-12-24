using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble360_2 : Distance
{
    [SerializeField] protected GameObject rayon;

    [SerializeField] private float timeBetweenTwoShots = 0.5f;

    [SerializeField] private List<GameObject> differentRadius = new List<GameObject>();

    //isShooting déclarée dans Distance

    protected void Start()
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
            case State.Chasing:
                isInRange();
                break;
            case State.Attacking:
                isInRange();
                StartCoroutine(CanShoot());
                break;
        }
    }



    //Voir Enemy.cs(héritage)
    protected override IEnumerator CanShoot()
    {
        if (isReadytoShoot)
        {
            isReadytoShoot = false;
            StartCoroutine("Tir");
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }
    }
    private IEnumerator Tir()
    {
        Instantiate(rayon, transform.position, Quaternion.identity);
        AddShoot();
        yield return new WaitForSeconds(timeBetweenTwoShots);
        Instantiate(rayon, transform.position, Quaternion.identity);
        AddShoot();
        
    }

    private void AddShoot()
    {
        differentRadius.Insert(0, rayon);
    }
}
