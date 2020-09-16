using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Transform target;
    private Vector3 dir;
    
    
    private void Awake()
    {
        //Get player reference;
       target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        GetDirection();
    }

    private void Update()
    {
        Lauch();
    }


    // recupère la direction à prendre
    private void GetDirection()
    {
        dir = (target.position - transform.position).normalized;
    }

    //envoie le projectile
    private void Lauch()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

