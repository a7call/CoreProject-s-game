using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautMS : Cac
{
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Chasing:
                StartCoroutine(PowerMode());
                isInRange();
                break;

            case State.Attacking:
                GetPlayerPos();
                StartCoroutine(BaseAttack());
                isInRange();
                break;
        }

    }

    #region Power Mode
    [SerializeField] private float distanceEnemyAggressive = 5f;
    private bool isPowerModeReady = true;
    [SerializeField] private float powerModeCd = 10f;
    // Variable sur la vitesse de déplacement
    [SerializeField] private float newSpeedEnemy = 3f;
    [SerializeField] private float speedDuration = 1.75f;
    private IEnumerator PowerMode()
    {
        float distanceEnemyPlayer = Vector3.Distance(target.transform.position, transform.position);
        if (distanceEnemyPlayer <= distanceEnemyAggressive && isPowerModeReady)
        {
            isPowerModeReady = false;
            yield return new WaitForSeconds(Random.Range(0, 1.5f));
            yield return StartCoroutine(IncreaseSpeed());
            yield return new WaitForSeconds(powerModeCd);
            isPowerModeReady = true;

        }
    }

    // Coroutine qui permet d'augmenter la vitesse de l'ennemi
    private IEnumerator IncreaseSpeed()
    {
        float baseMoveSpeed = aIPath.maxSpeed;
        newSpeedEnemy = 2 * baseMoveSpeed;
        aIPath.maxSpeed = newSpeedEnemy;
        yield return new WaitForSeconds(speedDuration);
        aIPath.maxSpeed = baseMoveSpeed;
    }
    #endregion

}
