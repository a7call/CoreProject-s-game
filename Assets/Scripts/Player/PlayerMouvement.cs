using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public float mooveSpeed;
    public Rigidbody2D rb;
    private Vector2 mouvement;
    
    private Vector3 velocity = Vector3.zero;
    public Animator animator;
    public float StartSmoothTime;
    public float StopSmoothTime;
    private Vector2 mouvementVector;

    public static PlayerMouvement instance;

    // Singleton
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("+ d'une instance de PlayerMouvement dans la scene");
            return;
        }
        else
        {
            instance = this;
        }
    }
    void Update()
    {
        CheckInputs();
        GetInputAxis();
        ClampMouvement(mouvement);
        GetLastDirection();
        SetAnimationVariable();


    }

    private void FixedUpdate()
    {
        MovePlayer(mouvementVector * mooveSpeed * Time.fixedDeltaTime);
        
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

}
