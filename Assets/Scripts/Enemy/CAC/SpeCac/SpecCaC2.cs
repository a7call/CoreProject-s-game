using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/// <summary>
/// Faire un loup qui quand il te chase (que t'es rentré dans sa zone d'aggro), il te diminue ta ms pendant 2 sec
/// Et lorsqu'il te touche la 1ere fois, il t'inflige saignememant/fear
/// Le mob est directement créer, et se met directement à chase le joueur
/// Lorsque le joueur est dans une zone dans laquelle le mob le détecte, le mob s'éneve
/// C'est là que sa ms augmente pendant 2 secondes (faisable qu'une fois)
/// Puis il continue de chase normalement si il a pas trouvé, sinon ca veut dire qu'il est à son cac
/// Alors il attaque
/// Sa première attaque infligement saignement pendant 5 secondes toutes les 1sec
/// Le cd de cette attaque est de 20sec
/// Si l'attaque est en Cd, il attaque classique
/// Sinon, il ré-utilise son power
/// </summary>

public class SpecCaC2 : Cac
{

    // Distance à partir de laquelle le loup active le PowerMode
    public float distanceWolfAggressive;
    // Distance entre le joueur et le loup
    private float distanceWolfPlayer;
    // Variable qui dit si le loup est en PowerMode ou non
    private bool isPowerMode = false;

    // Valeur de la nouvelle vitesse du player
    [SerializeField] private float decreaseSpeedPlayer;
    // Temps pendant laquelle il garde cette vitesse
    [SerializeField] private float speedDuration = 2f;

    //Récupérer la fonction de PlayerMovement
    private PlayerMouvement playerMouvement;

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
        // Vie initiale
        SetMaxHealth();
    }
    private void Update()
    {
        switch (currentState)
        {
            default:
            case State.Chasing:
                Aggro();
                MoveToPath();
                PowerMode();
                StartCoroutine(DecreasePlayerSpeed());
                isInRange();
                break;

            case State.Attacking:
                BaseAttack();
                GetPlayerPos();
                isInRange();
                break;
        }

    }

    // Fonction qui permet de savoir si le PowerMode s'active
    private void PowerMode()
    {
        distanceWolfPlayer = Vector3.Distance(target.transform.position, transform.position);
        if (distanceWolfPlayer <= distanceWolfAggressive) isPowerMode = true;
    }

    // Coroutine qui permet d'augmenter la vitesse
    private IEnumerator DecreasePlayerSpeed()
    {
        if (isPowerMode == true)
        {
            Vector3 speedDir = (target.transform.position - transform.position).normalized;
            playerMouvement.rb.velocity = decreaseSpeedPlayer * speedDir * Time.fixedDeltaTime;
            yield return new WaitForSeconds(speedDuration);
        }
        else
        {
            yield return null;
        }
    }

    // Find player to follow
    private void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //Mouvement

    // Override fonction Aggro ( Enemy.cs)  => aggro à l'initialisation
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