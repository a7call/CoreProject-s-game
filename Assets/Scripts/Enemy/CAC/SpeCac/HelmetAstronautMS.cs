using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautMS : Cac
{
    private float distanceEnemyPlayer;
    [SerializeField] private float distanceEnemyAggressive = 5f;
    // Variable qui dit si le loup est en PowerMode ou non
    private bool isPowerMode = false;
    private bool isSlowCdEnd = true;
    // Variable de temps au bout du quel il peut relancer le PowerMode
    private float powerModeTime = 10f;
    // Variable sur la vitesse de déplacement
    [SerializeField] private float newSpeedEnemy = 225f;
    [SerializeField] private float speedDuration = 1.75f;

    private PlayerMouvement playerMouvement;
    private Enemy enemy;

    private void Start()
    {
        playerMouvement = FindObjectOfType<PlayerMouvement>();
        enemy = FindObjectOfType<Enemy>();

        currentState = State.Chasing;
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
            case State.Chasing:
                Aggro();
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
        
        if (distanceEnemyPlayer <= distanceEnemyAggressive && isSlowCdEnd)
        {
            isSlowCdEnd = false;
            isPowerMode = true;
            yield return new WaitForSeconds(powerModeTime);
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
    //Mouvement

    // Override fonction Aggro (Enemy.cs)  => aggro à l'initialisation
    protected override void Aggro()
    {
        targetPoint = target;
    }


    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement


    // Voir Enemy.cs (héritage)

    protected override void PlayerInSight()
    {
        base.PlayerInSight();
    }

    // Health

    // Voir Enemy.cs (héritage)
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    // Voir Enemy.cs (héritage)
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    // Voir Enemy.cs (héritage)
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }


    // Voir Cac.cs (héritage)
    protected override void isInRange()
    {
        base.isInRange();
    }

    // Voir Enemy.cs (héritage)
    protected override void GetPlayerPos()
    {
        base.GetPlayerPos();
    }

    // Voir Cac.cs (héritage)
    protected override void BaseAttack()
    {
        base.BaseAttack();
    }

}
