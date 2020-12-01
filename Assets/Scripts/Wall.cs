using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Enemy[] enemies;

    private void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && ParasiteIdol.parasiteIdolFear)
        {
           collision.gameObject.GetComponent<Enemy>().currentState = Enemy.State.Chasing;
        }
    }
}
