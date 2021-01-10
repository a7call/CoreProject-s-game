using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class BossTentaclePop : MonoBehaviour
{
    [Header("Global Parameters")]
    // Health paramaters
    [SerializeField] private float maxHealth; // Write amount
    [SerializeField] private float currentHealth;
    [SerializeField] private Slider healthBar;

    // General Variables
    private Animator animator;
    private Rigidbody2D rb;

    // Player reference
    private PlayerMouvement playerMouvement;
    private PlayerHealth playerHealth;
    public Transform target;


    // Sprite Renderer
    private SpriteRenderer spriteRenderer; // GetLeComponent

    // PathFinding Parameters
    [HideInInspector]
    public float nextWayPointDistance = 0.05f;
    [HideInInspector]
    public AIPath aIPath;
    [HideInInspector]
    public AIDestinationSetter targetSetter;

    // Projectile parameters
    // Projectile de type Salive
    //[SerializeField] private GameObject saliveProjectile; // Ne pas faire comme ca, voir comment Lopez à fait
    // Projectile de type Pompe
    //[SerializeField] private GameObject[] listPumpProjectiles = new GameObject[3];
    // Projectile de type 360°
    // A configurer plutard. A priori, j'aimerai bien en tirer un nombre random (de 6 à 12 imaginons)




    [Header("FirstPhase Parameters")]
    [SerializeField] private float attackRange;

    [Header("SecondPhase Parameters")]
    [SerializeField] private float dps;

    private void Awake()
    {
        GetReference();
    }

    private void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    private void Update()
    {
        SetAnimationVariable();
        GetLastDirection();
        print("A");
    }

    private void GetReference()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        aIPath = GetComponent<AIPath>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetSetter = GetComponent<AIDestinationSetter>();
        targetSetter.target = target;
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();
    }

    private void SetAnimationVariable()
    {
        if (aIPath.canMove)
        {
            animator.SetFloat("HorizontalSpeed", aIPath.velocity.x);
            animator.SetFloat("VerticalSpeed", aIPath.velocity.y);
            float BossSpeed = aIPath.velocity.sqrMagnitude;
            animator.SetFloat("Speed", BossSpeed);
        }
        else
        {
            animator.SetFloat("HorizontalSpeed", 0);
            animator.SetFloat("VerticalSpeed", 0);
            float BossSpeed = 0;
            animator.SetFloat("Speed", BossSpeed);
        }
    }

    private void GetLastDirection()
    {
        if (aIPath.desiredVelocity.x > 0.1 || aIPath.desiredVelocity.x < 0.1 || aIPath.desiredVelocity.y < 0.1 || aIPath.desiredVelocity.y > 0.1)
        {
            animator.SetFloat("LastMoveX", targetSetter.target.position.x - rb.position.x);
            animator.SetFloat("LastMoveY", targetSetter.target.position.y - rb.position.y);
        }
    }

    

}
