using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackModule : ActiveObjects
{

    private bool isAlreadyFlying;
    public Transform packGFX;
    private Rigidbody2D rbPlayer;
    private GameObject player;
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        rbPlayer = player.GetComponent<Rigidbody2D>();
    }
    protected override void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.U) && readyToUse && !isAlreadyFlying)
        {
            StartCoroutine(StartFlying());
        }
        else if (Input.GetKeyDown(KeyCode.U) && isAlreadyFlying)
        {
            StartCoroutine(CdToReUse());
            readyToUse = false;
            StartCoroutine(StopFlying());

        }
    }
    
    private IEnumerator StartFlying()
    {       
        GameObject.FindGameObjectWithTag("PlayerShadow").transform.parent = null;
        rbPlayer.AddForce(new Vector2(0, 300));
        yield return new WaitForSeconds(0.1f);
        GameObject.FindGameObjectWithTag("PlayerShadow").transform.parent = player.transform;
        isAlreadyFlying = true;

    }
    private IEnumerator StopFlying()
    {
        GameObject.FindGameObjectWithTag("PlayerShadow").transform.parent = null;
        rbPlayer.AddForce(new Vector2(0, -300));
        yield return new WaitForSeconds(0.1f);
        GameObject.FindGameObjectWithTag("PlayerShadow").transform.parent = player.transform;
        isAlreadyFlying = false;

    }
}
