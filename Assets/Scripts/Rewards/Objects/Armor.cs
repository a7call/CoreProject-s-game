using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Rewards
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
        base.OnTriggerEnter2D(collision);
    }
}
