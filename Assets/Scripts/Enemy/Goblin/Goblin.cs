using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    [SerializeField] protected EnemyScriptableObject enemyData;

    private bool canGoToPos = false;
    private bool isDyingCoroutine = false;

    // Timers des couroutines
    private float fearTime = 1f;
    private float afkTime = 4f;
    private float starterTime = 2f;
    private float timeBetweenFlashes = 1f;

    // Compteurs des courotines
    private int counter = 0;
    // Mettre à 3
    private int stack = 3;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        currentState = State.Chasing;
        aIPath.canMove = false;
        SetData();
        SetMaxHealth();
        StartCoroutine(Starter());
    }

    private void SetData()
    {
        aIPath.maxSpeed = Random.Range(enemyData.moveSpeed, enemyData.moveSpeed + 1f);

        maxHealth = enemyData.maxHealth;
        whiteMat = enemyData.whiteMat;
        defaultMat = enemyData.defaultMat;
    }

    protected override void Update()
    {
        base.Update();

        switch (currentState)
        {
            case State.Chasing:
                aIPath.canMove = false;
                if(counter != stack)
                {
                    StartCoroutine(RunningAway());
                }
                else if(counter == stack && isDyingCoroutine)
                {
                    StartCoroutine(DestroyEnemy());
                }
                break;
        }
    }

    private IEnumerator RunningAway()
    {
        if (canGoToPos)
        {
            counter += 1;
            canGoToPos = false;
            Fear();
            yield return new WaitForSeconds(fearTime);
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(afkTime);
            canGoToPos = true;
        }

        if (counter == stack)
        {
            isDyingCoroutine = true;
        }
    }

    private IEnumerator Starter()
    {
        yield return new WaitForSeconds(starterTime);
        canGoToPos = true;
    }

    private IEnumerator DestroyEnemy()
    {
        isDyingCoroutine = false;
        yield return new WaitForSeconds(timeBetweenFlashes);
        for (int i = 1; i < 6; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - 0.2f * i);
            yield return new WaitForSeconds(timeBetweenFlashes);
        }
        Destroy(gameObject);
    }

    protected override void Fear()
    {
        float moveSpeed = aIPath.maxSpeed * 100;
        direction = (player.transform.position - gameObject.transform.position).normalized;
        rb.velocity = -direction * moveSpeed * Time.fixedDeltaTime;
    }

}
