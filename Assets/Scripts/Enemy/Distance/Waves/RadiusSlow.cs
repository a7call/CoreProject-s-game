using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RadiusSlow : RadiusGrowUp
{

    [SerializeField] private float maxRadius = 5;
    private PlayerHealth playerHealth;
    private PlayerMouvement playerMouvement;

    protected override void Start()
    {
        playerMouvement = FindObjectOfType<PlayerMouvement>();
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
        return base.RadiusGrowByTime();
    }

    protected override void ShootRadius()
    {

        if (radius < maxRadius)
        {
            hits = Physics2D.CircleCastAll(transform.position, RadiusGrowByTime(), Vector2.zero, Mathf.Infinity, hitLayer);
        }
        else
        {
            print("A");
            hits = Physics2D.CircleCastAll(transform.position, maxRadius, Vector2.zero, Mathf.Infinity, hitLayer);
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                changeSpeed();
            }

            // Détruire les objets qui sont destructibles
        }
    }

    private void changeSpeed()
    {
        float newMoveSpeed = 100f;
        playerMouvement.mooveSpeed = newMoveSpeed;
    }

}
