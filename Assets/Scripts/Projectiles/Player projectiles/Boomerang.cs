using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
 
    private GameObject player;
    public float speed;
    private Vector3 dir;

    private float distance;
    private Transform playerTransform;


    [SerializeField]
    private float backDistance;
    private bool isComingBack;

    private void Awake()
    {
         player = GameObject.FindGameObjectWithTag("Player");
         PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
         playerTransform = player.GetComponent<Transform>();
         dir = (playerAttack.attackPoint.position - playerTransform.position).normalized;

    }
    private void Start()
    {
    }

    private void Launch()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
    private void Update()
    {
       distance = Vector3.Distance(transform.position, playerTransform.position);
       Launch();
       if(distance > backDistance) isComingBack = true;
       if (isComingBack) dir = (playerTransform.position - transform.position).normalized;
       if (distance < 0.2f) Destroy(gameObject);       
    }
}
