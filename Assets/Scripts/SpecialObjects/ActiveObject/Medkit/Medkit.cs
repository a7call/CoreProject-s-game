using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : ActiveObjects
{

    private PlayerHealth playerHealth;
    private PlayerMouvement playerMouvement;

    private float timeCantMoove = 1.5f;
    private bool isHealing = false;
    private bool canWalk = true;


    protected override void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();
    }
    protected override void Update()
    {
        //if (ModuleAlreadyUse)
        //{
        //    DestroyObject();
        //}

        if (Input.GetKeyDown(KeyCode.U))
        {
            UseModule = true;
            isHealing = true;
        }

        if (UseModule)
        {
            UseMedkit();
        }
    }
    private IEnumerator CantMoove(float _timeCantMoove)
    {
        playerMouvement.mooveSpeed = 0;
        yield return new WaitForSeconds(_timeCantMoove);
        canWalk = false;
        playerMouvement.mooveSpeed = 100; // On remet 100 car on veut remettre la valeur de vitesse initiale dans tous les cas !!!
        UseModule = false;
        ModuleAlreadyUse = true;
    }

    private IEnumerator CoroutineDeSoin(float _timeMedkit)
    {
        isHealing = false;
        playerHealth.currentHealth += 1;
        yield return new WaitForSeconds(_timeMedkit);
        isHealing = true;
    }

    private void UseMedkit()
    {
        if (playerHealth.maxHealth == 6)
        {
            if (canWalk)
            {
                StartCoroutine(CantMoove(timeCantMoove));
            }
            if (isHealing)
            {
                StartCoroutine(CoroutineDeSoin(0.25f));
            }
        }
        else if (playerHealth.maxHealth == 8)
        {
            if (canWalk)
            {
                StartCoroutine(CantMoove(timeCantMoove));
            }
            if (isHealing)
            {
                StartCoroutine(CoroutineDeSoin(0.1875f));
            }
        }
    }
}
