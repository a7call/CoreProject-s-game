using System.Collections;
using UnityEngine;
/// <summary>
/// Classe gérant les mouvements du joueur (se référer à Lopez)
/// </summary>
public class PlayerMouvement : Player
{

    public static bool isModuleInertie;

    private Vector2 mouvement;

    //Changement de velocity de privée à public
    public Vector3 velocity = Vector3.zero;
    public float StartSmoothTime;
    public float StopSmoothTime;
    private Vector2 mouvementVector;
    private PlayerEnergy playerEnergy;

    //SpeedShoesModule
    [HideInInspector]
    protected bool SpeedAlreadyUp = false;
    [HideInInspector]
    public static bool isSpeedShoesModule;
    [HideInInspector]
    public static float SpeedMultiplier;

    protected override void Awake()
    {
        base.Awake();
        playerEnergy = GetComponent<PlayerEnergy>();
    }
    protected override void Update()
    {

        base.Update();

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

                if (Input.GetMouseButtonDown(1) && playerEnergy.currentEnergy >= dashEnergyCost)
                {
                    StartCoroutine(DashCo());
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

    [SerializeField] private float DashDuration;
    private bool isDashing;
    private IEnumerator DashCo()
    {
        if (!isDashing)
        {
            Vector2 dir = new Vector2(mouvement.x, mouvement.y);
            playerEnergy.SpendEnergy(dashEnergyCost);
            isDashing = true;
            GetComponent<BoxCollider2D>().enabled = false;
            rb.AddForce(dir * dashForce*Time.deltaTime, ForceMode2D.Impulse);
            yield return new WaitForSeconds(DashDuration);
            if (isModuleInertie) CoroutineManager.Instance.StartCoroutine(ModuleInertie.InertieCo());
            isDashing = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }
    protected override void NormalMode()
    {
        base.NormalMode();
    }
}
