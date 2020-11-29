using System.Collections;
using UnityEngine;
/// <summary>
/// Classe gérant les mouvements du joueur (se référer à Lopez)
/// </summary>
public class PlayerMouvement : Player
{

    public static bool isModuleInertie;

    private Vector2 mouvement;
    private bool isCorotinePlaying=false;
    [SerializeField]private bool canDash = true;

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

    [HideInInspector]
    public static bool isArretTemporelActive = false;


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
                GetInputAxis();
                ClampMouvement(mouvement);
                GetLastDirection();
                SetAnimationVariable();

                MakesEnergy();
                LastStack();

                if (canDash)
                {
                    if(Input.GetMouseButton(1))
                    {
                        playerEnergy.energyIsReloading = false;
                        Dash();
                        if (isCorotinePlaying == false)
                        {
                            StartCoroutine(Coro());
                        }
                    }
                }

                break;

            case EtatJoueur.fear:
                    break;

            case EtatJoueur.shopping:
                //Definir tout ce qu'on veut faire dedans
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
    void GetInputAxis()
    {
        mouvement.x = Input.GetAxisRaw("Horizontal");
        mouvement.y = Input.GetAxisRaw("Vertical");
       
    }

    // Get last Direction for Idle
    void GetLastDirection()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == -1 || Input.GetAxisRaw("Vertical") == 1)
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

    [SerializeField] private float DashDuration = 0f;
    private bool isDashing;
    //private IEnumerator DashCo()
    //{
    //    if (!isDashing)
    //    {
    //        Vector2 dir = new Vector2(mouvement.x, mouvement.y);
    //        playerEnergy.SpendEnergy(dashEnergyCost);
    //        isDashing = true;
    //        GetComponent<BoxCollider2D>().enabled = false;
    //        rb.AddForce(dir * dashForce*Time.deltaTime, ForceMode2D.Impulse);
    //        yield return new WaitForSeconds(DashDuration);
    //        if (isModuleInertie) CoroutineManager.Instance.StartCoroutine(ModuleInertie.InertieCo());
    //        isDashing = false;
    //        GetComponent<BoxCollider2D>().enabled = true;
    //    }
        
    //}

    private void MakesEnergy()
    {
        if (Input.GetMouseButtonUp(1) && playerEnergy.currentEnergy < playerEnergy.maxEnergy)
        {
            playerEnergy.energyIsReloading = true;
        }
    }

    private void LastStack()
    {
        if (playerEnergy.energyIsReloading && playerEnergy.currentStack == 0)
        {
            canDash = false;
        }
        else if (playerEnergy.energyIsReloading && playerEnergy.currentStack != 0)
        {
            canDash = true;
        }
    }

    private IEnumerator Coro()
    {
        isCorotinePlaying = true;
        playerEnergy.SpendEnergy(2.5f);
        yield return new WaitForSeconds(0.1f);
        isCorotinePlaying = false;
    }

    private void Dash()
    {
        if (canDash)
        {
            print("A");
            // FAIRE LA FONCTION DE DASH
        }
    }

}
