using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautFear : Cac
{
    private PlayerHealth playerHealth;
    private PlayerMouvement playerMouvement;

    // Variables liées à l'attaque qui fear
    private bool isFirstAttack = true;
    private bool isFearCdEnd = true;
    private bool isFear = false;
    [SerializeField] private float loadDelay = 20f;
    [SerializeField] private float fearTime = 2f;
    [SerializeField] private float distanceFear = 4f;

    private Transform playerTransform;
    private Transform enemyTransform;

    private Vector3 vecEnemyPlayer;
    private Vector3 pointPos;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();

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
                isInRange();
                MoveToPath();
                if (isFear)
                {
                    DistancePlayerFearPoint();
                }
                break;

            case State.Attacking:
                GetPlayerPos();
                if (isFear)
                {
                    DistancePlayerFearPoint();
                }
                if (isFirstAttack)
                {
                    StartCoroutine(FearMode());
                    StartCoroutine(FearAttack());

                }
                BaseAttack();
                GetPlayerPos();
                isInRange();
                break;
        }

    }

    // Point dans lequel on va fear
    public void positionFearPoint()
    {
        playerTransform = playerMouvement.GetComponent<Transform>();
        enemyTransform = gameObject.transform;

        vecEnemyPlayer = (enemyTransform.position - playerTransform.position).normalized;

        pointPos = playerTransform.position - distanceFear * vecEnemyPlayer;
    }

    // Disntance joueur-FearPoint
    private void DistancePlayerFearPoint()
    {
        float distance = Vector3.Distance(target.position, pointPos);
        if (distance <= 0.2) playerMouvement.rb.velocity = Vector3.zero;
    }

    private IEnumerator FearMode()
    {
        positionFearPoint();
        isFear = true;
        isFearCdEnd = false;
        playerMouvement.currentEtat = PlayerMouvement.EtatJoueur.fear;
        yield return new WaitForSeconds(fearTime);
        playerMouvement.currentEtat = PlayerMouvement.EtatJoueur.normal;
        isFear = false;
        isFearCdEnd = true;
    }

    // Première attaque du State Attacking qui Fear le joueur
    private IEnumerator FearAttack()
    {
        isFirstAttack = false;
        Vector3 pos = pointPos;
        Vector3 targetPos = targetPoint.position;
        Vector3 direction = (pos - targetPos).normalized;
        playerMouvement.rb.velocity = direction * playerMouvement.mooveSpeed * Time.fixedDeltaTime;
        yield return new WaitForSeconds(loadDelay);
        isFirstAttack = true;
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
    protected override void TakeDamage(int _damage)
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
        rb.velocity = Vector2.zero;
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayers);

        foreach (Collider2D h in hits)
        {
            //Take dammage
        }
    }

}
