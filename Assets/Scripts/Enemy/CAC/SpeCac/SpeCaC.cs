using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeCaC : Cac
{
    // mob to instentiate  sur l'aggro
    public GameObject mobs;

    // check si les mobs ont spawn 
    private bool spawned = false;

    

    private void Start()
    {
        // Set data
        SetData();
        // Set initial targetPoint
        SetFirstPatrolPoint();
        // Vie initial
        SetMaxHealth();
    }
    private void Update()
    {
        // Suit player si n'attaque pas et ne charge pas 
        if(!isCharging && !isInAttackRange) MoveToPath();
        // Patrouille
        Patrol();
        // Voir fonction
        Aggro();
        // Voir fonction
        StartCoroutine(Charge());
        // Check player position 
        GetPlayerPos();
        // Check distance pour attaque
        isInRange();

    }

    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement


    // Aggro si pas entrain de charger + Intentiate trash (BaseCaC.cs) 
    protected override void Aggro()
    {

        if (!isCharging && Vector3.Distance(transform.position, target.position) < inSight)
        {
            if (!spawned)
            {
                spawned = true;
                GameObject.Instantiate(mobs, transform.position, Quaternion.identity);
            }
            isPatroling = false;
            targetPoint = target;

        }
    }

    // Voir Enemy.cs (héritage)
    protected override void Patrol()
    {
        base.Patrol();
    }

    // Voir Enemy.cs (héritage)
    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }


    // Health

    // Voir Enemy.cs (héritage)
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    // Voir Enemy.cs (héritage)
    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    // Voir Enemy.cs (héritage)
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }



    //Attack

    [SerializeField] public float chargeSpeed;
    [SerializeField] public float loadDelay;
    [SerializeField] public float restTime;
    [SerializeField] public float readyToChargeTimer;
    [SerializeField] public bool isCharging;
    [SerializeField] public bool readyToCharge = false;
    [SerializeField] public bool isFinished = true;


    // Couroutine de la charge 
    private IEnumerator Charge()
    {
        if (readyToCharge && !isCharging &&!isPatroling)
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
            // Active la charche 
            rb.velocity = chargeDir * chargeSpeed * Time.fixedDeltaTime;
            // Sécurité 
            yield return new WaitForSeconds(restTime);
            // Ne charge plus 
            isCharging = false;
            // Prépare la prochiane charge
            yield return new WaitForSeconds(readyToChargeTimer);
            readyToCharge = true;

        }
        else
        {
            yield return null;
        }

    }
    // Voir Cac.cs (héritage)
    protected override void isInRange()
    {
        base.isInRange();
    }
    // Voir Enemy.cs (héritage)
    protected override void GetPlayerPos()
    {
        base.GetPlayerPos();
    }
    // Voir Cac.cs (héritage)
    protected override void BaseAttack()
    {
        base.BaseAttack();
    }


}
