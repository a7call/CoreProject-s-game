using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHeart : Rewards
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.GetComponent<Player>().AddLifePlayer(2);
        base.OnTriggerEnter2D(collision);
    }
}
