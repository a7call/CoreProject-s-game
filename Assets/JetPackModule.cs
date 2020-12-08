using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackModule : ActiveObjects
{

    private bool isAlreadyFlying;
    public Transform packGFX;
    private Rigidbody2D rbPlayer;
    private GameObject player;
    private Transform shadow;
    private float distance;
    private bool isShadowSet;
    Vector3 currentPos;
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        rbPlayer = player.GetComponent<Rigidbody2D>();
        shadow = GameObject.FindGameObjectWithTag("PlayerShadow").transform;
    }
    protected override void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.U) && readyToUse && !isAlreadyFlying)
        {
           StartFlying();
        }
        else if (Input.GetKeyDown(KeyCode.U) && isAlreadyFlying)
        {
            StartCoroutine(CdToReUse());
            readyToUse = false;
            StopFlying();

        }
        if(!isAlreadyFlying) shadow.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.53f);
        if (isShadowSet)
        {
            shadow.position = new Vector2(player.transform.position.x, currentPos.y);
            
        }

    }
    
    private void StartFlying()
    {
        currentPos = shadow.position;
        rbPlayer.AddForce(new Vector2(0, 300));
        isAlreadyFlying = true;
        isShadowSet = true;

    }   
    private void StopFlying()
    {
        rbPlayer.AddForce(new Vector2(0, -300));
        isAlreadyFlying = false;

    }
}
