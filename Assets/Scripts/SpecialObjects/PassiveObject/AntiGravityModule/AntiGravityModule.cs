using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityModule : PassiveObjects
{
    private PlayerMouvement player;
    [SerializeField] private float moveSpeedMultiplier = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        player.mooveSpeed *= moveSpeedMultiplier;
    }
}
