using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    protected GameObject player;
    protected Vector3 dir;
    protected Transform playerTransform;
    protected PlayerAttack playerAttack;
    [SerializeField]
    protected float speed;


    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttack = player.GetComponent<PlayerAttack>();
        playerTransform = player.GetComponent<Transform>();
       // dir = (playerAttack.attackPoint.position - playerTransform.position).normalized;

    }
}
