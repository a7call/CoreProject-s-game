using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Rewards
{
    protected bool isRoomClear = true;
    protected float speed = 4;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.goldPlayer += 1;

        }
        base.OnTriggerEnter2D(collision);
    }

    protected void Update()
    {
        if (isRoomClear)
        {
            Player player = FindObjectOfType<Player>();
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
}
