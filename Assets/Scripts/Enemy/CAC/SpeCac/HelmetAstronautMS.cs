using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautMS : Cac
{
    private float distanceEnemyPlayer;
    [SerializeField] private float distanceEnemyAggressive = 5f;
    private bool isPowerMode = false;
    private bool isPowerModeReady = true;
    // Variable de temps au bout du quel il peut relancer le PowerMode
    private float powerModeTime = 5f;
    private float powerModeCd = 10f;
    // Variable sur la vitesse de déplacement
    [SerializeField] private float newSpeedEnemy = 225f;
    [SerializeField] private float speedDuration = 1.75f;

    private PlayerMouvement playerMouvement;
    private Enemy enemy;

    private void Start()
    {
        playerMouvement = FindObjectOfType<PlayerMouvement>();
        enemy = FindObjectOfType<Enemy>();

        currentState = State.Patrolling;
        // Get Player Reference
        FindPlayer();
        // Set target
        targetPoint = target;
        // Set data
        SetData();
        SetMaxHealth();
    }


    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            default:
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                MoveToPath();
                StartCoroutine(PowerMode());
                if (isPowerMode) StartCoroutine(IncreaseSpeed());
                isInRange();
                break;

            case State.Attacking:
                GetPlayerPos();
                BaseAttack();
                isInRange();
                break;
        }

    }
    // Coroutine qui permet de mettre en place le PowerMode
    private IEnumerator PowerMode()
    {
        distanceEnemyPlayer = Vector3.Distance(target.transform.position, transform.position);
        
        if (distanceEnemyPlayer <= distanceEnemyAggressive && isPowerModeReady)
        {
            isPowerModeReady = false;
            isPowerMode = true;
            yield return new WaitForSeconds(powerModeTime);
            isPowerMode = false;
            yield return new WaitForSeconds(powerModeCd);
            isPowerModeReady = true;

        }
    }

    // Coroutine qui permet d'augmenter la vitesse de l'ennemi
    private IEnumerator IncreaseSpeed()
    {
        isPowerMode = false;
        float baseMoveSpeed = enemy.moveSpeed;
        enemy.moveSpeed = newSpeedEnemy;
        yield return new WaitForSeconds(speedDuration);
        enemy.moveSpeed = baseMoveSpeed;
    }

    // Find player to follow
    private void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
