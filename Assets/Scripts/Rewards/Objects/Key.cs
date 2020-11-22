using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Rewards
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.numberOfKeys++;
        }
        base.OnTriggerEnter2D(collision);
    }
}
