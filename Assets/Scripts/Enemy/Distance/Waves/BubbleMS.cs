using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BubbleMS : Distance
{
    private PlayerHealth playerHealth;
    [SerializeField] protected GameObject rayon;

    [SerializeField] private List<GameObject> differentRadius = new List<GameObject>();
    
    private bool firstShoot = true;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        currentState = State.Chasing;
        // Set premier targetPoint
        SetFirstPatrolPoint();
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
                Aggro();
                isInRange();
                MoveToPath();
                if(!firstShoot) rb.velocity = Vector2.zero;
                break;
            case State.Attacking:
                isInRange();
                StartCoroutine(CanShoot());
                break;
        }
    }

    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement

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

    // Voir Enemy.cs (héritage)
    protected override void Patrol()
    {
        base.Patrol();
    }

    // Voir Enemy.cs (héritage)
    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }


    // Health


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


    // Attack

    // Voir Enemy.cs (héritage)
    protected override void ResetAggro()
    {
        base.ResetAggro();
    }

    //Voir Enemy.cs(héritage)
    protected override IEnumerator CanShoot()
    {
        if (isReadytoShoot && firstShoot)
        {
            isReadytoShoot = false;
            firstShoot = false;
            rb.velocity = Vector2.zero;
            Shoot();
            yield return new WaitForSeconds(restTime);
        }
    }

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        Instantiate(rayon, transform.position, Quaternion.identity);
        AddShoot();
    }

    private void AddShoot()
    {
        differentRadius.Insert(0, rayon);
    }
}
