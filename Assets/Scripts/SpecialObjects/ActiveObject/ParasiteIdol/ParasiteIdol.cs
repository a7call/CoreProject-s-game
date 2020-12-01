using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteIdol : ActiveObjects
{
    GameObject[] enemiesInRoom;

    protected override void Start()
    {
        enemiesInRoom = GameObject.FindGameObjectsWithTag("Enemy");
    }

    protected override void Update()
    {
        if(Input.GetKeyDown(KeyCode.U) && readyToUse)
        {
            FearEnemy();
        }
    }

    private void FearEnemy()
    {
        foreach (GameObject enemy in enemiesInRoom)
        {
            enemy.gameObject.GetComponent<Enemy>().currentState = Enemy.State.Feared;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
