using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlowProjectile : PlayerProjectiles
{
    [SerializeField] private float slowMultiplier;

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        Enemy enemy = collision.GetComponent<Enemy>();
    //        if (!enemy.IsSlowed)
    //        {
    //            enemy.IsSlowed = true;
    //            enemy.AIMouvement.MoveForce *= slowMultiplier;


    //        }
    //    }
    //    base.OnTriggerEnter2D(collision);

    //}
}
