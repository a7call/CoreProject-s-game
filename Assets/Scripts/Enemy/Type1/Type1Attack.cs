using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Attack : EnemyAttack
{
    [SerializeField] protected Type1ScriptableObject Type1Data;

    private float chargeSpeed;
    private float loadDelay;
    private float restTime;
    private float chargeDelay;
    private float readyToChargeTimer;
    public Rigidbody2D rb;
    [SerializeField] public bool isCharging;
    [SerializeField] public bool readyToCharge = false;
    [SerializeField] public bool isFinished = true;


    protected void Start()
    {
        SetData();
    }

    private void FixedUpdate()
    {
        StartCoroutine(Charge());
    }

    protected override void Update()
    {
        StartCoroutine(ChargeTimer());
    }


    // Couroutine de la récuperation de la charge
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
    protected void SetData()
    {
        chargeSpeed = Type1Data.chargeSpeed;
        loadDelay = Type1Data.loadDelay;
        restTime = Type1Data.restTime;
        chargeDelay = Type1Data.chargeDelay;
        readyToChargeTimer = Type1Data.readyToChargeTimer;
        attackRange = Type1Data.attackRange;
        attackRadius = Type1Data.attackRadius;
        hitLayers = Type1Data.hitLayers;
    }
}
