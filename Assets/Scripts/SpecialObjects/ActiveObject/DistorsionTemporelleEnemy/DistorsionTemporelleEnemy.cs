using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistorsionTemporelleEnemy : ActiveObjects
{
    public bool isDistorsionTemporelleEnemy;
    private float timeDistorsionTemporelle = 10f;

    //private float moveDiviser = 0.5f;

    private GameObject[] enemies;
    //private List<GameObject> projectile = new List<GameObject>();
    private GameObject[] projectilsEnemy;

    protected override void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        projectilsEnemy = GameObject.FindGameObjectsWithTag("EnemyProjectil");
    }

    protected override void Update()
    {
        projectilsEnemy = GameObject.FindGameObjectsWithTag("EnemyProjectil");

        if (Input.GetKeyDown(KeyCode.U) && readyToUse)
        {
            //Enemy.isDistorsionTemporelle = true;
            //Enemy.moveSpeedDiviser = moveDiviser;
            isDistorsionTemporelleEnemy = true;
            StartCoroutine(DistorsionTemporelle());
        }

        //if (isDistorsionTemporelleEnemy)
        //{
        //    StartCoroutine(DistorsionTemporelle());
        //}

        if (ModuleAlreadyUse)
        {
            isDistorsionTemporelleEnemy = false;
            Destroy(gameObject);
        }
    }

    private IEnumerator DistorsionTemporelle()
    {
        print("A");
        readyToUse = false;

        foreach (GameObject enemy in enemies)
        {
            Rigidbody2D rb = enemy.gameObject.GetComponent<Enemy>().GetComponent<Rigidbody2D>();
            Vector2 velocity = rb.velocity;
            velocity = velocity / 2;
        }

        yield return new WaitForSeconds(timeDistorsionTemporelle);
        ModuleAlreadyUse = true;
    }
}
