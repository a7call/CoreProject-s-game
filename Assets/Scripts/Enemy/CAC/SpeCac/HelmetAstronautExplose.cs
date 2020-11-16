using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautExplose : Cac
{
    [SerializeField] private float distanceToExplode = 2f;
    [SerializeField] private float timeToExplode = 1f;

    

    private void Start()
    {
        

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

        AliveOrNot();

        base.Update();
        switch (currentState)
        {
            default:
            case State.Chasing:
                Aggro();
                isInRange();
                MoveToPath();
                break;

            case State.Attacking:
                GetPlayerPos();
                StartCoroutine(Attack());
                break;
        }

    }

    private void AliveOrNot()
    {
        if (currentHealth <= 0) BaseAttack();
    
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
        print("Destruction ennemy");
    }

    private IEnumerator Attack()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(timeToExplode);
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);

        foreach (Collider2D h in hits)
        {
            playerHealth.TakeDamage(20);
        }

        Destroy(gameObject);
        print("Destruction ennemy");
    }


}
