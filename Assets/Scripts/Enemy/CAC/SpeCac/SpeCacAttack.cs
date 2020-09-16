using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeCacAttack : Type1Attack
{

    [SerializeField] public float chargeSpeed;
    [SerializeField] public float loadDelay;
    [SerializeField] public float restTime;
    [SerializeField] public float chargeDelay;
    [SerializeField] public float readyToChargeTimer;
    [SerializeField] public bool isCharging;
    [SerializeField] public bool readyToCharge = false;
    [SerializeField] public bool isFinished = true;


    private void FixedUpdate()
    {
        StartCoroutine(Charge());
    }

    private void Update()
    {
        StartCoroutine(ChargeTimer());
        GetPlayerPos();
        isInRange();
    }

    private void Start()
    {
        SetData();
    }

    private IEnumerator ChargeTimer()
    {
        if (isFinished)
        {
            isFinished = false;
            readyToCharge = true;
            yield return new WaitForSeconds(chargeDelay);
            readyToCharge = false;
            yield return new WaitForSeconds(readyToChargeTimer);
            isFinished = true;

        }

    }


    // Couroutine de la charge 
    private IEnumerator Charge()
    {
        if (readyToCharge && !isCharging)
        {
            isCharging = true;
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(loadDelay);
            Vector3 chargeTarget = target.position;
            Vector3 chargeDir = (chargeTarget - transform.position).normalized;
            rb.velocity = chargeDir * chargeSpeed * Time.fixedDeltaTime;
            yield return new WaitForSeconds(restTime);
            isCharging = false;

        }
        else
        {
            yield return null;
        }

    }

    protected override void isInRange()
    {
        base.isInRange();
    }

    protected override void GetPlayerPos()
    {
        base.GetPlayerPos();
    }

    protected override void BaseAttack()
    {
        base.BaseAttack();
    }

    protected override void SetData()
    {
        base.SetData();
    }

   
}
