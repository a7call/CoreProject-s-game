using System.Collections;
using UnityEngine;

using UnityEngine.InputSystem;

/// <summary>
/// Classe gérant les mouvements du joueur (se référer à Lopez)
/// </summary>
public class PlayerMouvement : Player
{

    public static bool isModuleInertie;

    private Vector2 mouvement;


    //Changement de velocity de privée à public
    [HideInInspector]
    public Vector3 velocity = Vector3.zero;
    public float StartSmoothTime;
    public float StopSmoothTime;
    private Vector2 mouvementVector;
    private PlayerEnergy playerEnergy;

    //SpeedShoesModule
    protected bool SpeedAlreadyUp = false;
    public static bool isSpeedShoesModule;
    public static float SpeedMultiplier;

    //PiercedPocketModule
    public static bool isPiercedPocketModule; 

    [HideInInspector]
    public static bool isArretTemporelActive = false;

    public static object instance { get; internal set; }

    protected override void Awake()
    {
        base.Awake();
        playerEnergy = GetComponent<PlayerEnergy>();
    }
    protected  void Update()
    {

        if (isSpeedShoesModule && !SpeedAlreadyUp)
        {
            SpeedAlreadyUp = true;
            mooveSpeed *= SpeedMultiplier;
        }

            switch (currentEtat)
        {
            default:
                break;

            case EtatJoueur.normal:
                CheckInputs();
                //GetInputAxis();
                ClampMouvement(mouvement);
                GetLastDirection();
                SetAnimationVariable();
                break;

            case EtatJoueur.fear:
                    break;

            case EtatJoueur.shopping:
                //Definir tout ce qu'on veut faire dedans
                break;

            case EtatJoueur.AFK:
                canDash = false;
                break;

            case EtatJoueur.Grapping:
                break;

        }

    }

    private void FixedUpdate()
    {
        switch (currentEtat)
        {
            default:

                break;

            case EtatJoueur.normal:
                MovePlayer(mouvementVector * mooveSpeed * Time.fixedDeltaTime);
                break;

            case EtatJoueur.fear:
                break;

            case EtatJoueur.shopping:
                rb.velocity = Vector2.zero;
                break;

            case EtatJoueur.Grapping:
                break;

            case EtatJoueur.AFK:
                rb.velocity = Vector2.zero;
                break;

            case EtatJoueur.Dashing:
                PiercedPocketActivation();
                break;
        }
        
    }



    //Smooth Player mouvement
    void MovePlayer(Vector2 _mouvement)
    {
        Vector3 targetVelocity = _mouvement;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, StartSmoothTime);
    }


    // Same Speed when Input (x,y)
    void ClampMouvement(Vector2 _mouvement)
    {
      
       mouvementVector = Vector2.ClampMagnitude(_mouvement, 1);
    }

    // Animation Variable
    void SetAnimationVariable()
    {
        animator.SetFloat("HorizontalSpeed", mouvement.x);
        animator.SetFloat("VerticalSpeed", mouvement.y);
        float playerSpeed = mouvement.sqrMagnitude;
        animator.SetFloat("Speed", playerSpeed);
    }

    // Get Input
    //void GetInputAxis()
    //{
    //    mouvement.x = Input.GetAxisRaw("Horizontal");
    //    mouvement.y = Input.GetAxisRaw("Vertical");

    //}

    // Get last Direction for Idle
    void GetLastDirection()
    {
        if (mouvement.x == 1 || mouvement.x == -1 || mouvement.y == -1 || mouvement.y == 1)
        {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));

        }
    }

    // Check If Player released Inputs
    void CheckInputs()
    {
        if (mouvement == Vector2.zero)
        {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector2.zero, ref velocity, StopSmoothTime);
        }
    }

    
    public bool canDash = true;
    public float timeBetweenDashes= 0.5f;
    private IEnumerator Dash()
    {
        if (canDash && playerEnergy.currentStack > 0)
        {
            Vector2 dir = new Vector2(mouvement.x, mouvement.y);
            if(dir!= Vector2.zero)
            {
                currentEtat = EtatJoueur.Dashing;
                playerEnergy.SpendEnergy(1);
                canDash = false;
                rb.AddForce(dir * dashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                LayerMask projectilLayerMask = 3;
                Physics2D.IgnoreLayerCollision(gameObject.layer, projectilLayerMask);
                yield return new WaitForSeconds(DashTime);
                Physics2D.IgnoreLayerCollision(gameObject.layer, projectilLayerMask, false);
                currentEtat = EtatJoueur.normal;
                yield return new WaitForSeconds(timeBetweenDashes);
                canDash = true;
            }
            
        }     
    }

    void PiercedPocketActivation()
    {
        PiercedPocketModule pockets = FindObjectOfType<PiercedPocketModule>();
        if (pockets)
        {
            if (pockets.bombsReady) StartCoroutine(pockets.SpawnBombs());
        }
    }


    public void OnHorizontal(InputValue val)
    {
        mouvement.x = val.Get<float>();
    }

    public void OnVertical(InputValue val)
    {
        mouvement.y = val.Get<float>();
    }

    public void OnDash()
    {
        StartCoroutine(Dash());

        //PlayerControl variable;
        //variable.Player.Reload.actionMap.AddBinding("<Keyboard/a");
    }

   
    
}
