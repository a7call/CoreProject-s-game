﻿using UnityEngine;
/// <summary>
/// Classe gérant les mouvements du joueur (se référer à Lopez)
/// </summary>
public class PlayerMouvement : MonoBehaviour
{

    public Rigidbody2D rb;
    private Vector2 mouvement;

    //Changement de velocity de privée à public
    public Vector3 velocity = Vector3.zero;
    public Animator animator;
    public float StartSmoothTime;
    public float StopSmoothTime;
    private Vector2 mouvementVector;
    private PlayerEnergy playerEnergy;
    public float mooveSpeed;

    public PlayerScriptableObjectScript playerData;

    public EtatMouvementJoueur currentMouvementState = EtatMouvementJoueur.normal;

    public enum EtatMouvementJoueur
    {
        normal,
        fear,
    }

    private void Awake()
    {
        playerEnergy = GetComponent<PlayerEnergy>();
        mooveSpeed = playerData.mooveSpeed;
    }
    void Update()
    {
        switch (currentMouvementState)
        {
            default:
                break;

            case EtatMouvementJoueur.normal:
                CheckInputs();
                GetInputAxis();
                ClampMouvement(mouvement);
                GetLastDirection();
                SetAnimationVariable();

                if (Input.GetKeyDown(KeyCode.C) && playerEnergy.currentEnergy >= playerData.dashEnergyCost)
                {
                    Dash();
                }

                break;

            case EtatMouvementJoueur.fear:
                    break;
        }

    }

    private void FixedUpdate()
    {
        switch (currentMouvementState)
        {
            default:
                break;

            case EtatMouvementJoueur.normal:
                MovePlayer(mouvementVector * mooveSpeed * Time.fixedDeltaTime);
                break;

            case EtatMouvementJoueur.fear:
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


    void Dash()
    {   
        Vector2 dir = new Vector2(mouvement.x, mouvement.y);
        playerEnergy.SpendEnergy(playerData.dashEnergyCost);
        rb.AddForce(dir * playerData.dashForce);
    }

}
