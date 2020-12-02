using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteIdol : ActiveObjects
{
    GameObject[] enemiesInRoom;
    public static bool parasiteIdolFear = false;
    [SerializeField] private float fearTime = 5f;

    protected override void Start()
    {
        enemiesInRoom = GameObject.FindGameObjectsWithTag("Enemy");
    }

    protected override void Update()
    {
        if(Input.GetKeyDown(KeyCode.U) && readyToUse)
        {
            UseModule = true;
            parasiteIdolFear = true;
            StartCoroutine(ResetStateChasing());
        }

        if (ModuleAlreadyUse)
        {
            Destroy(gameObject);
        }
    }

    private void FearEnemy()
    {
        foreach (GameObject enemy in enemiesInRoom)
        {
            enemy.gameObject.GetComponent<Enemy>().currentState = Enemy.State.Feared;
        }
    }
    private void EnemyChasing()
    {
        foreach (GameObject enemy in enemiesInRoom)
        {
            enemy.gameObject.GetComponent<Enemy>().currentState = Enemy.State.Chasing;
        }
    }

    private IEnumerator ResetStateChasing()
    {
        readyToUse = false;
        FearEnemy();
        yield return new WaitForSeconds(fearTime);
        EnemyChasing();
        UseModule = false;
        ModuleAlreadyUse = true;
    }


}
