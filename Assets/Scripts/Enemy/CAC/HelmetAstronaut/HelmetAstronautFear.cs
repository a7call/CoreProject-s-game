using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A refaire ou à supprimer !
public class HelmetAstronautFear : Cac
{

    // Variables liées à l'attaque qui fear
    private bool isFirstAttack = true;
    private bool isFearCdEnd = true;
    private bool isFear = false;
    [SerializeField] private float loadDelay = 20f;
    [SerializeField] private float fearTime = 2f;
    [SerializeField] private float distanceFear = 4f;

    private Transform playerTransform;
    private Transform enemyTransform;

    private Vector3 vecEnemyPlayer;
    private Vector3 pointPos;

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            default:
            case State.Patrolling:
                break;
            case State.Chasing:
                isInRange();
                if (isFear)
                {
                    DistancePlayerFearPoint();
                }
                break;

            case State.Attacking:
                GetPlayerPos();
                if (isFear)
                {
                    DistancePlayerFearPoint();
                }
                if (isFirstAttack)
                {
                    StartCoroutine(FearMode());
                    StartCoroutine(FearAttack());

                }
                StartCoroutine(BaseAttack());
                GetPlayerPos();
                isInRange();
                break;
        }

    }

    // Point dans lequel on va fear
    public void positionFearPoint()
    {
        playerTransform = player.GetComponent<Transform>();
        enemyTransform = gameObject.transform;

        vecEnemyPlayer = (enemyTransform.position - playerTransform.position).normalized;

        pointPos = playerTransform.position - distanceFear * vecEnemyPlayer;
    }

    // Disntance joueur-FearPoint
    private void DistancePlayerFearPoint()
    {
        float distance = Vector3.Distance(target.position, pointPos);
        if (distance <= 0.2) player.rb.velocity = Vector3.zero;
    }

    private IEnumerator FearMode()
    {
        positionFearPoint();
        isFear = true;
        isFearCdEnd = false;
        player.currentEtat = Player.EtatJoueur.fear;
        yield return new WaitForSeconds(fearTime);
        if (IsDontFuckWithMe)
        {
            Destroy(gameObject);
        }
        player.currentEtat = Player.EtatJoueur.normal;
        isFear = false;
        isFearCdEnd = true;
    }

    // Première attaque du State Attacking qui Fear le joueur
    private IEnumerator FearAttack()
    {
        isFirstAttack = false;
        Vector3 pos = pointPos;
        Vector3 targetPos = target.position;
        Vector3 direction = (pos - targetPos).normalized;
        player.rb.velocity = direction * player.mooveSpeed * Time.fixedDeltaTime;
        yield return new WaitForSeconds(loadDelay);
        isFirstAttack = true;
    }
}
