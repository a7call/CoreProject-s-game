using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetAstronautMS : Cac
{
    protected override void Update()
    {

    }
    // LE POWER MODE EST DESACTIVER LE TEMPS DE TROUVER UN EFFET OU UNE ANIMATION POUR LE REPRESENTER 
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
        float baseMoveSpeed = AIMouvement.Speed;
        newSpeedEnemy = 2 * baseMoveSpeed;
        AIMouvement.Speed = newSpeedEnemy;
        yield return new WaitForSeconds(speedDuration);
        AIMouvement.Speed = baseMoveSpeed;
    }
    #endregion
    protected override IEnumerator BaseAttack()
    {
        yield return new WaitForSeconds(2f);
        attackAnimationPlaying = false;
    }
}
