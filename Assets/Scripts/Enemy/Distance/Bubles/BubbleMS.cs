using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BubbleMS : Distance
{
    [SerializeField] protected GameObject rayon;

    [SerializeField] private List<GameObject> differentRadius = new List<GameObject>();
    
    private bool firstShoot = true;
    [SerializeField] private bool canDie = false;

    private AngleProjectile AngleProjectile;
    [SerializeField] GameObject[] projectiles;

    private float timeShoot = 6f;
    private int angleTir = 360;

    void Start()
    {
        GetProjectile();
        currentState = State.Patrolling;
        // Set data
        SetData();
        SetMaxHealth();
    }
    protected override void Update()
    {
        print(currentHealth);
        EnabledRayon();

        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                isInRange();
                if(!firstShoot) rb.velocity = Vector2.zero;
                break;
            case State.Attacking:
                isInRange();
                StartCoroutine(CanShoot());
                break;

            case State.Paralysed:
                rb.velocity = Vector2.zero;
                break;

            case State.KnockedBack:
                rb.velocity = Vector2.zero;
                break;

            case State.Freeze:
                rb.velocity = Vector2.zero;
                break;

            case State.Feared:
                Fear();
                break;

        }

    }

    public override void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        StartCoroutine(WhiteFlash());
        if (currentHealth < 1)
        {
            CoroutineManager.Instance.StartCoroutine(Die());
            SpawnRewards();
        }
    }

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
        GameObject monRayon = Instantiate(rayon, transform.position, Quaternion.identity);
        monRayon.transform.parent = gameObject.transform;
        AddShoot();
    }

    private void AddShoot()
    {
        differentRadius.Insert(0, rayon);
    }

    private void EnabledRayon()
    {
        if (ParasiteIdol.parasiteIdolFear)
        {
            if(gameObject.transform.GetChild(1) != null) 
            {
                // gameObject.transform.GetChild(1).GetComponent<RadiusGrowUp>().enabled = false;
                GameObject myRayon = GameObject.FindGameObjectWithTag("RayonMS");
                Destroy(myRayon);
            }
            ParasiteIdol.parasiteIdolFear = false;
            firstShoot = true;
            isReadytoShoot = true;
        }
    }

    private IEnumerator Die()
    {
        print("A");
        canDie = true;
        
        float decalage = angleTir / (projectiles.Length - 1);
        AngleProjectile.angleDecalage = -decalage * (projectiles.Length + 1) / 2;

        //base.Shoot();
        for (int i = 0; i < projectiles.Length; i++)
        {
            AngleProjectile.angleDecalage = AngleProjectile.angleDecalage + decalage;
            GameObject myProjectile = GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
        }

        yield return new WaitForSeconds(timeShoot);
        print("test");
    }

    private void GetProjectile()
    {
        foreach (GameObject projectile in projectiles)
        {
            AngleProjectile = projectile.GetComponent<AngleProjectile>();
        }
    }

}
