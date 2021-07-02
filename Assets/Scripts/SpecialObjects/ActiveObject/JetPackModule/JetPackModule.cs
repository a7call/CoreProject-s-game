using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackModule : CdObjects
{

    private bool isAlreadyFlying;
    private Rigidbody2D rbPlayer;
    private Transform shadow;
    [SerializeField] private float hauteurDeVole;
    [SerializeField] private float ShadowPosY;
    private bool isShadowSet;
    private bool hasStartingTofly;
    private bool isStoppingfly;
    Vector3 playerCurrentPos;
    Vector3 currentShadowPos;
    bool isFlying;
    protected override void Start()
    {
        base.Start();
        shadow = GameObject.FindGameObjectWithTag("PlayerShadow").transform;
        rbPlayer = player.GetComponent<Rigidbody2D>();
        
    }
    protected override void Update()
    {
        if (isFlying)
        {

        }
        
        if (UseModule && !isAlreadyFlying)
        {
            StartCoroutine(StartFlying());
            UseModule = false;
            isFlying = true;
        }
        else if (UseModule && isAlreadyFlying)
        {
            UseModule = false;
            isFlying = false;
            StartCoroutine(StopFlying());

        }
        if (hasStartingTofly && Vector3.Distance(playerCurrentPos, player.transform.position) < hauteurDeVole)
        {

            FlyUp();
        }
        else
        {
            hasStartingTofly = false;
        }

        if (isStoppingfly && Vector3.Distance(playerCurrentPos, player.transform.position) < hauteurDeVole)
        {
            GoDown();
        }
        else
        {
            isStoppingfly = false;
        }
       

        if (!isAlreadyFlying) shadow.position = new Vector2(player.transform.position.x, player.transform.position.y - ShadowPosY);
        if (isShadowSet)
        {
            shadow.position = new Vector2(player.transform.position.x, player.transform.position.y - hauteurDeVole - ShadowPosY);
        }

    }
    
    private IEnumerator StartFlying()
    {
        isAlreadyFlying = true;
        currentShadowPos = shadow.position;
        playerCurrentPos = player.transform.position;
        hasStartingTofly = true;
        yield return new WaitForSeconds(0.3f);
        isShadowSet = true;


    }   
    private IEnumerator StopFlying()
    {
        isShadowSet = false;
        currentShadowPos = shadow.position;
        playerCurrentPos = player.transform.position;
        isStoppingfly = true;
        yield return new WaitForSeconds(0.3f);
        isAlreadyFlying = false;

    }

    private void FlyUp()
    {
        player.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 10f);
        rbPlayer.velocity = Vector2.zero;
        shadow.position = new Vector2(player.transform.position.x, currentShadowPos.y);
    }

    private void GoDown()
    {
        player.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 10f);
        rbPlayer.velocity = Vector2.zero;
        shadow.position = new Vector2(player.transform.position.x, currentShadowPos.y);
    }
}
