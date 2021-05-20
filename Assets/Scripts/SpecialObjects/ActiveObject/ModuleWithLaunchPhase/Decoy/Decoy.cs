using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : ModuleLauchPhase
{
    private Player player;

    [SerializeField] private float timeBeforDesactivation;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask hit;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }
    protected override void Update()
    {
        base.Update();
        if (isNotMoving && !isAlreadyActive)
        {
            isAlreadyActive = true;
            //StartCoroutine(DecoyFunction());
        }
    }

}
