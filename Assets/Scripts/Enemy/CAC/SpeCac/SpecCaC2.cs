using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.WSA;

/// <summary>
/// Faire un loup qui quand il te chase (que t'es rentré dans sa zone d'aggro), il te diminue ta ms pendant 2 sec
/// Et lorsqu'il te touche la 1ere fois, il t'inflige saignememant/fear
/// Le mob est directement créer, et se met directement à chase le joueur
/// Lorsque le joueur est dans une zone dans laquelle le mob le détecte, le mob s'éneve
/// C'est là que la ms du joueur diminue (faisable qu'une fois)
/// Puis il continue de chase normalement si il a pas trouvé, sinon ca veut dire qu'il est à son cac
/// 
/// 
/// Alors il attaque
/// Sa première attaque infligement un fear toutes les X secondes
/// Le cd de cette attaque est de 20sec
/// Si l'attaque est en Cd, il attaque classique
/// Sinon, il ré-utilise son power
/// </summary>

public class SpecCaC2 : Cac
{
    // Distance à partir de laquelle le loup active le PowerMode, déclarer en [SerializeField] pour la changer rapidement
    // A retirer du [SerializeField] par la suite
    [SerializeField] private float distanceWolfAggressive = 5;
    // Distance entre le joueur et le loup
    private float distanceWolfPlayer;
    // Variable qui dit si le loup est en PowerMode ou non
    private bool isPowerMode = false;
    private bool isSlowCdEnd = true;
    // Variable de temps au bout du quel il peut relancer le PowerMode
    private float powerModeTime = 10f;

    // Valeur de la nouvelle vitesse du player
    [SerializeField] private float newSpeedPlayer = 75f;
    // Temps pendant laquelle il garde cette vitesse
    [SerializeField] private float speedDuration = 2f;

    //Récupérer la fonction de PlayerMovement
    private PlayerMouvement playerMouvement;

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


    private void Start()
    {
        playerMouvement = FindObjectOfType<PlayerMouvement>();

        currentState = State.Chasing;
        // Get Player Reference
        FindPlayer();
        // Set target
        targetPoint = target;
        // Set data
        SetData();
        SetMaxHealth();
    }

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            default:
            case State.Chasing:
                Aggro();
                MoveToPath();
                StartCoroutine(PowerMode());
                if(isPowerMode) StartCoroutine(DecreasePlayerSpeed());
                isInRange();
                if (isFear)
                {
                    DistancePlayerFearPoint();
                }
                break;

            case State.Attacking:
                if (isFear) 
                {
                    DistancePlayerFearPoint();
                }
                if (isFirstAttack) 
                {
                    StartCoroutine(FearMode());
                    StartCoroutine(FearAttack());
                    
                }
                BaseAttack();
                GetPlayerPos();
                isInRange();
                break;

        }

    }

    // Réduction de la MS du joueur pendant le State Chasing

    // Fonction qui permet de savoir si le PowerMode du mob s'active
    private IEnumerator PowerMode()
    {  
        distanceWolfPlayer = Vector3.Distance(target.transform.position, transform.position);
        if (distanceWolfPlayer <= distanceWolfAggressive && isSlowCdEnd)
        {
            isSlowCdEnd = false;
            isPowerMode = true;
            yield return new WaitForSeconds(powerModeTime);
        }
    }
       

    // Coroutine qui permet de diminuer la vitesse du Joueur
    private IEnumerator DecreasePlayerSpeed()
    {
        isPowerMode = false;
        float baseMouveSpeed = playerMouvement.mooveSpeed;
        playerMouvement.mooveSpeed = newSpeedPlayer;
        yield return new WaitForSeconds(speedDuration);
        playerMouvement.mooveSpeed = baseMouveSpeed;
    }


    private IEnumerator FearMode()
    {
        positionFearPoint();
        isFear = true;
        isFearCdEnd = false;
        playerMouvement.currentEtat = PlayerMouvement.EtatJoueur.fear;
        yield return new WaitForSeconds(fearTime);
        playerMouvement.currentEtat = PlayerMouvement.EtatJoueur.normal;
        isFear = false;
        isFearCdEnd = true;
    }

    // Première attaque du State Attacking qui Fear le joueur

    private IEnumerator FearAttack()
    {
        isFirstAttack = false;
        Vector3 pos = pointPos;
        Vector3 targetPos = targetPoint.position;
        Vector3 direction = (pos - targetPos).normalized;
        playerMouvement.rb.velocity = direction * playerMouvement.mooveSpeed * Time.fixedDeltaTime;
        yield return new WaitForSeconds(loadDelay);
        isFirstAttack = true;
    }

    public void positionFearPoint()
    {
        playerTransform = playerMouvement.GetComponent<Transform>();
        enemyTransform = gameObject.transform;

        vecEnemyPlayer = (enemyTransform.position - playerTransform.position).normalized;

        pointPos = playerTransform.position - distanceFear * vecEnemyPlayer;
    }

    private void DistancePlayerFearPoint()
    {
        float distance = Vector3.Distance(target.position, pointPos);
            if (distance <=0.2) playerMouvement.rb.velocity = Vector3.zero;
    }

    // Find player to follow
    private void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //Mouvement

    // Override fonction Aggro (Enemy.cs)  => aggro à l'initialisation
    protected override void Aggro()
    {
        targetPoint = target;
    }


    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement


    // Voir Enemy.cs (héritage)

    protected override void PlayerInSight()
    {
        base.PlayerInSight();
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