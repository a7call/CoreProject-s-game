using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalftHeart : Rewards
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.GetComponent<Player>().AddLifePlayer(1);
        base.OnTriggerEnter2D(collision);
    }
}
