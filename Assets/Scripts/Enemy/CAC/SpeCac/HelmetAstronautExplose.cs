using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautExplose : Cac
{

    [SerializeField] private float distanceToExplode = 0.2f;
    [SerializeField] private bool firstAttack = true;

    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

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
                print(Vector3.Distance(targetPoint.transform.position, transform.position));
                break;

            case State.Attacking:
                GetPlayerPos();
                BaseAttack();
                break;
        }

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
        if (Vector3.Distance(targetPoint.transform.position, transform.position) < distanceToExplode)
        {
            currentState = State.Attacking;
        }
        else
        {
            currentState = State.Chasing;
        }
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
            playerHealth.TakeDamage(20);
        }

        Destroy(gameObject);
    }


}
