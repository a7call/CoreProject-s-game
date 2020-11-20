using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Cac.cs
/// Elle contient en plus de Cac une coroutine de charge
/// </summary>
public class SpeCaC : Cac
{
    // mob to instantiate  sur l'aggro
    public GameObject mobs;

    // check si les mobs ont spawn 
    private bool spawned = false;

    

    private void Start()
    {
        currentState = State.Patrolling;
        // Set data
        SetData();
        // Set initial targetPoint
        SetFirstPatrolPoint();
        SetMaxHealth();
    }
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Patrolling:
                MoveToPath();
                PlayerInSight();
                break;

            case State.Chasing:
                Aggro();
                if(!isCharging) MoveToPath();
                StartCoroutine(Charge());
                isInRange();
                break;

            case State.Attacking:
                StartCoroutine(BaseAttack());
                GetPlayerPos();
                isInRange();
                break;


        }       

    }


    // Aggro si pas entrain de charger + Instantiate trash (BaseCaC.cs) 
    // Quaternion.identy = pas de rotation de l'objet instantié lors de sa création. L'objet est aligné avec le monde et ses parents
    protected override void Aggro()
    {
            if (!spawned)
            {
                spawned = true;
                Instantiate(mobs, transform.position, Quaternion.identity);
            }
            targetPoint = target;
    }



    //Attack

    [SerializeField] public float chargeSpeed;
    [SerializeField] public float loadDelay;
    [SerializeField] public float restTime;
    [SerializeField] public float readyToChargeTimer;
    [SerializeField] public bool isCharging;
    [SerializeField] public bool readyToCharge = true;


    // Couroutine de la charge 
    private IEnumerator Charge()
    {
        if (readyToCharge && !isCharging )
        {
            // Ne peut plus charger
            readyToCharge = false;
            // L'ennemi est entrain de charger
            isCharging = true;
            // L'ennemi s'arrete
            rb.velocity = Vector2.zero;
            // Charge la charge
            yield return new WaitForSeconds(loadDelay);
            // Lock la target + direction
            Vector3 chargeTarget = target.position;
            Vector3 chargeDir = (chargeTarget - transform.position).normalized;
            // Active la charge 
            rb.velocity = chargeDir * chargeSpeed * Time.fixedDeltaTime;
            // Sécurité 
            yield return new WaitForSeconds(restTime);
            // Ne charge plus 
            isCharging = false;
            // Prépare la prochaine charge
            yield return new WaitForSeconds(readyToChargeTimer);
            readyToCharge = true;

        }
        else
        {
            yield return null;
        }

    }
}
