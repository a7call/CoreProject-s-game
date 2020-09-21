using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeCaC : Cac
{

    public GameObject mobs;
    private bool spawned = false;

    

    private void Start()
    {
        SetData();
        SetFirstPatrolPoint();
        SetMaxHealth();
    }
    private void Update()
    {
        Patrol();
        Aggro();
        StartCoroutine(ChargeTimer());
        StartCoroutine(Charge());
        GetPlayerPos();
        isInRange();

    }

    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement


    // Aggro si pas entrain de charger
    protected override void Aggro()
    {

        Vector3 dir = (targetToFollow.position - transform.position).normalized;

        if (!isCharging && Vector3.Distance(transform.position, targetToFollow.position) < aggroDistance)
        {
            if (!spawned)
            {
                spawned = true;
                GameObject.Instantiate(mobs, transform.position, Quaternion.identity);
            }
            isPatroling = false;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
        }
    }


    protected override void Patrol()
    {
        base.Patrol();
    }
  
    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }


    // Health

    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }



    //Attack

    [SerializeField] public float chargeSpeed;
    [SerializeField] public float loadDelay;
    [SerializeField] public float restTime;
    [SerializeField] public float chargeDelay;
    [SerializeField] public float readyToChargeTimer;
    [SerializeField] public bool isCharging;
    [SerializeField] public bool readyToCharge = false;
    [SerializeField] public bool isFinished = true;

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


}
