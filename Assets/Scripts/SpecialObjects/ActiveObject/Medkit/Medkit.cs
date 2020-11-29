using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : ActiveObjects
{

    private PlayerHealth playerHealth;
    private PlayerMouvement playerMouvement;

    private float timeMedkit = 1.5f;
    private float timeCantMoove = 9f;
    private bool isHealing = false;
    private bool canWalk = true;


    protected override void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();
    }
    protected override void Update()
    {
        if (ModuleAlreadyUse)
        {
            DestoyMedkit();
        }

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
        float normalSpeed = playerMouvement.mooveSpeed;
        canWalk = false;
        playerMouvement.mooveSpeed = 0;
        yield return new WaitForSeconds(_timeCantMoove);
        playerMouvement.mooveSpeed = normalSpeed;
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
                StartCoroutine(CantMoove(9f));
            }
            if (isHealing)
            {
                StartCoroutine(CoroutineDeSoin(1.125f));
            }
        }
        else if (playerHealth.maxHealth == 8)
        {
            if (canWalk)
            {
                StartCoroutine(CantMoove(9f));
            }
            if (isHealing)
            {
                StartCoroutine(CoroutineDeSoin(9 / 8f));
            }
        }
    }
    private void DestoyMedkit()
    {
        Destroy(gameObject);
    }
}
