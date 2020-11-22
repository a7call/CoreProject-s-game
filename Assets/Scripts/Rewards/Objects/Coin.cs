using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Rewards
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.goldPlayer += 1;

        }
        base.OnTriggerEnter2D(collision);
    }
}
