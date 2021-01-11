using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class BossTentaclePop : Enemy
{
    [Header("Global Parameters")]
    [SerializeField] private BossScriptableObject BossData;
    private float restTime;
    private int nbTir;
    private GameObject projectile; // Mettre le projectile classique
    // Health paramaters
    // maxHealth, currentHealth, healthBar

    // General Variables
    // Animator, rb

    // Player reference
    // playerMouvement, playerHealth, target

    // Sprite Renderer
    // spriteRenderer

    // PathFinding Parameters

    // Projectile

    // Projectile parameters
    // Projectile de type Salive
    //[SerializeField] private GameObject saliveProjectile; // Ne pas faire comme ca, voir comment Lopez à fait
    // Projectile de type Pompe
    //[SerializeField] private GameObject[] listPumpProjectiles = new GameObject[3];
    // Projectile de type 360°
    // A configurer plutard. A priori, j'aimerai bien en tirer un nombre random (de 6 à 12 imaginons)


    //[Header("Pattern Parameters")]
    //[SerializeField] private bool isShooting;
    //[SerializeField] private bool isReadyToShoot;
    //[SerializeField] private float attackRange;

    //[Header("SecondPhase Parameters")]
    //[SerializeField] private float dps;



    protected override void Awake()
    {
        base.Awake();
        SetData();
    }

    private void Start()
    {
        currentState = State.Chasing;
        SetMaxHealth();
        //Shoot();
    }

    // Deux states uniquement, chasing + attacking
    protected override void Update()
    {
        switch (currentState)
        {
            case State.Chasing:
                isInRange();
                print("Chasing");
            break;

            case State.Attacking:
                isInRange();
                print("Attacking");
                //Shoot();
                break;

        }
        healthBar.SetHealth(currentHealth);
        SetAnimationVariable();
        GetLastDirection();
    }

    protected override void GetReference()
    {
        healthBarGFX.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetSetter = GetComponent<AIDestinationSetter>();
        targetSetter.target = target;
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();
    }
    private void SetData()
    {
        maxHealth = BossData.maxHealth;
        whiteMat = BossData.whiteMat;
        defaultMat = BossData.defaultMat;

        attackRange = BossData.attackRange;
        restTime = BossData.restTime;
        timeToSwitch = BossData.timeToSwich;
        nbTir = BossData.nbTir;
        projectile = BossData.projectile;
    }

    private void Shoot()
    {
        GameObject myProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        myProjectile.transform.parent = gameObject.transform;
    }



}
