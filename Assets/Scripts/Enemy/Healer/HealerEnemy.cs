using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HealerEnemy : Enemy
{
    [SerializeField] List<GameObject> enemyInRoom = new List<GameObject>();
    [SerializeField] protected EnemyScriptableObject EnemyData;
    private bool isSeachingForEnemyToHeal = false;
    private bool isHealing = false;
    private float amountHeal = 1f;
    private float timeBetweenHeal = 1f;
    private float radiusOfDetection = 8f;
    [SerializeField] LayerMask enemyLayer;
    private Enemy enemyCurrentlyHealed = null;
    void Start()
    {      
        SetData();
        SetMaxHealth();
    }
    void SetData()
    {
        aIPath.maxSpeed = Random.Range(EnemyData.moveSpeed, EnemyData.moveSpeed + 1);
        maxHealth = EnemyData.maxHealth;
        whiteMat = EnemyData.whiteMat;
        defaultMat = EnemyData.defaultMat;
        attackRange = EnemyData.attackRange;
    }
    protected override void GetReference()
    {
        healthBarGFX.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetSetter = GetComponent<AIDestinationSetter>();
        targetSetter.target = transform;
        player = FindObjectOfType<Player>();
        player = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Patrolling:
                break;
            
            case State.Chasing:
                GetEnemyToHeal();
                break;

            case State.Attacking:
                break;
        }
    }


    private void GetAllEnemisInRoom()
    {
        Collider2D[] enemyInRoomArr = Physics2D.OverlapCircleAll(transform.position, radiusOfDetection, enemyLayer);
        foreach(Collider2D enemy in enemyInRoomArr)
        {
            enemyInRoom.Insert(0, enemy.gameObject);
        }
    }


    private void GetEnemyToHeal()
    {
        
        if (!isSeachingForEnemyToHeal )
        {
            GetAllEnemisInRoom();
            isSeachingForEnemyToHeal = true;
            foreach (GameObject enemy in enemyInRoom.ToArray())
            {
                if (enemy == null)
                {
                    enemyInRoom.Remove(enemy);
                    continue;
                }
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if(enemyScript.currentHealth < enemyScript.maxHealth && !isHealing)
                {
                    isHealing = true;
                    isSeachingForEnemyToHeal = false;
                    enemyCurrentlyHealed = enemyScript;
                    StartCoroutine(HealingCo(enemyScript, amountHeal, timeBetweenHeal)) ;
                }
                else
                {

                    continue;
                }

            }
            isSeachingForEnemyToHeal = false;
            enemyInRoom.Clear();
        }
        

    }

    private IEnumerator HealingCo(Enemy _enemy, float _amountToHeal, float _timeBetweenHeal)
    {
        while (_enemy.currentHealth < _enemy.maxHealth)
        {
            yield return new WaitForSeconds(_timeBetweenHeal);
            if (_enemy == null)
            {
                isHealing = false;
                break;
            }
            _enemy.currentHealth += _amountToHeal;  
            if (enemyCurrentlyHealed.currentHealth == enemyCurrentlyHealed.maxHealth)
            {
                isHealing = false;
                yield break;
            }
        }

    }
}
    
