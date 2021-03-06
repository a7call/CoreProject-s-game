﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileTeteChercheuse : ExplosionProjectile
{
    [SerializeField] protected float directionUpdateTime;
    [SerializeField] protected float detectionRadius;
    [SerializeField] private float angularSpeed = 0f;
    private GameObject lockedEnemy;
    private bool isEnemyLocked;
    public Rigidbody2D rb;


    protected override void Update()
    {
        base.Update();
        LockEnemy();
    }


   

    protected override void Launch()
    {
        if (isEnemyLocked)
        {
            if (lockedEnemy == null) return;
            Vector2 direction = ((Vector2)lockedEnemy.transform.position - rb.position);
            direction.Normalize();
            float rotationAmount = Vector3.Cross(direction, (transform.up * directionTir.y + transform.right * directionTir.x)).z;
            rb.angularVelocity = -rotationAmount * angularSpeed;
            rb.velocity = (transform.up * directionTir.y + transform.right * directionTir.x) * projectileSpeed;
            angularSpeed++;
        }
        else
        {
            transform.Translate(directionTir * projectileSpeed * Time.deltaTime, Space.World);
        }
    }

    private void LockEnemy()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, detectionRadius, weaponLayer);

        if (enemy != null)
        {
            isEnemyLocked = true;
            lockedEnemy = enemy.gameObject;
        }
    }
}
