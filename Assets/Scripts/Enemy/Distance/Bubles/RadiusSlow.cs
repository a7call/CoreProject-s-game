using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RadiusSlow : RadiusGrowUp
{

    [SerializeField] private float maxRadius = 5f;
    [SerializeField] private float newPlayerMoveSpeed = 100f;
    private PlayerMouvement playerMouvement;

    protected override void Start()
    {
        playerMouvement = FindObjectOfType<PlayerMouvement>();
        float playerMoveSpeed = playerMouvement.mooveSpeed;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override float Timer()
    {
        return base.Timer();
    }

    protected override float RadiusGrowByTime()
    {
        if (radius < maxRadius)
        {
            radius = radius = initialRadius + coeffDir * Timer();
            return radius;
        }
        else
        {
            return radius = maxRadius;
        }
    }

    protected override void ShootRadius()
    {
        float realMoveSpeed = playerMouvement.playerData.mooveSpeed;
        hits = Physics2D.CircleCastAll(transform.position, RadiusGrowByTime(), Vector2.zero, Mathf.Infinity, hitLayer);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (Vector3.Distance(transform.position, hit.transform.position) <= RadiusGrowByTime())
                {

                    playerMouvement.mooveSpeed = newPlayerMoveSpeed;
                }
                else
                {
                    playerMouvement.mooveSpeed = realMoveSpeed;
                }
                // Détruire les objets qui sont destructibles
            }
        }

    }
}
