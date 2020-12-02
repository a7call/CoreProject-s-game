using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleToInventory : Rewards
{
    [SerializeField] private  GameObject BlackHole;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print(collision);
            inventory.BlackHoles.Add(BlackHole);
        }
        base.OnTriggerEnter2D(collision);
    }
}
