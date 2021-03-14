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
        Animation();

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

    protected Vector3 screenMousePos;
    protected Vector3 screenPlayerPos;
    Vector3 horizon = new Vector3(1, 0, 0);

    // Animation Variable
    void Animation()
    {
        
        float playerSpeed = mouvement.sqrMagnitude;
        animator.SetFloat("Speed", playerSpeed);

        // position de la souris sur l'écran 
        screenMousePos = Input.mousePosition;
        // position du player en pixel sur l'écran 
        screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        // position du point d'attaque 
        Vector3 dir = new Vector3((screenMousePos - screenPlayerPos).x, (screenMousePos - screenPlayerPos).y);

        float angle = Quaternion.FromToRotation(Vector3.left, horizon - dir).eulerAngles.z;

        
        if (angle > 45 && angle <= 135)
        {
            animator.SetFloat("VerticalSpeed", 1);
            animator.SetFloat("HorizontalSpeed", 0);
        }
        else if (angle > 135 && angle <= 225)
        {
            animator.SetFloat("HorizontalSpeed", -1);
            animator.SetFloat("VerticalSpeed", 0);
        }
        else if (angle > 225 && angle <= 315)
        {
            animator.SetFloat("VerticalSpeed", -1);
            animator.SetFloat("HorizontalSpeed", 0);
        }
        else
        {
            animator.SetFloat("HorizontalSpeed", 1);
            animator.SetFloat("VerticalSpeed", 0);
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
