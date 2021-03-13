using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautMS : Cac
{
    private float distanceEnemyPlayer;
    [SerializeField] private float distanceEnemyAggressive = 5f;
    private bool isPowerMode = false;
    private bool isPowerModeReady = true;
    private bool canPowerMode = false;
    // Variable de temps au bout du quel il peut relancer le PowerMode
    private float powerModeTime = 5f;
    private float powerModeCd = 10f;
    // Variable sur la vitesse de déplacement
    [SerializeField] private float newSpeedEnemy = 3f;
    [SerializeField] private float speedDuration = 1.75f;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(enabledPowerMode());
    }


    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
         
            case State.Chasing:
                if (canPowerMode) StartCoroutine(PowerMode());
                if (isPowerMode) StartCoroutine(IncreaseSpeed());
                isInRange();
                break;

            case State.Attacking:
                GetPlayerPos();
                StartCoroutine(BaseAttack());
                isInRange();
                break;
        }

    }
    // Coroutine qui permet de mettre en place le PowerMode
    private IEnumerator PowerMode()
    {
        distanceEnemyPlayer = Vector3.Distance(target.transform.position, transform.position);
        if (distanceEnemyPlayer <= distanceEnemyAggressive && isPowerModeReady)
        {
            
            isPowerModeReady = false;
            isPowerMode = true;
            yield return new WaitForSeconds(powerModeTime);
            isPowerMode = false;
            yield return new WaitForSeconds(powerModeCd);
            isPowerModeReady = true;

        }
    }

    private IEnumerator enabledPowerMode()
    {
        yield return new WaitForSeconds(2f);
        canPowerMode = true;
    }

    // Coroutine qui permet d'augmenter la vitesse de l'ennemi
    private IEnumerator IncreaseSpeed()
    {
     
        isPowerMode = false;
        float baseMoveSpeed = aIPath.maxSpeed;
        newSpeedEnemy = 2 * baseMoveSpeed;
        aIPath.maxSpeed = newSpeedEnemy;
        yield return new WaitForSeconds(speedDuration);
        aIPath.maxSpeed = baseMoveSpeed;
    }

}
