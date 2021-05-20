using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBomb : ExplosivesModule
{
    [SerializeField] private float timeBeforActivation = 0f;

    private List<GameObject> enemiesSlowed;

    protected override void Start()
    {
        base.Start();
        enemiesSlowed = new List<GameObject>();

    }
    
    private void Activation()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().AIMouvement.speed /= 2f;
            enemiesSlowed.Add(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().mooveSpeed /= 2f;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().AIMouvement.speed *= 2f;
            enemiesSlowed.Remove(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().mooveSpeed *= 2f;
        }
    }

    protected override void Update()
    {
        base.Update();
        if(isNotMoving && !isAlreadyActive)
        {
            print("test");
            isAlreadyActive = true;
            Invoke("Activation", timeBeforActivation);
            CoroutineManager.Instance.StartCoroutine(ExplosionOnEnemy());
        }
    }
 
}
