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
    public float smoothTime;
    private Vector2 mouvementVector;

   
    void Update()
    {
        mouvement.x = Input.GetAxis("Horizontal");
        mouvement.y = Input.GetAxis("Vertical");
        mouvementVector = Vector2.ClampMagnitude(mouvement, 1);

    }

    private void FixedUpdate()
    {
        MovePlayer(mouvementVector * mooveSpeed * Time.fixedDeltaTime);
        animator.SetFloat("horizontalSpeed", mouvement.x);
        animator.SetFloat("verticalSpeed", mouvement.y);
        float playerSpeed = rb.velocity.sqrMagnitude;
        animator.SetFloat("Speed", playerSpeed);
    }

    void MovePlayer(Vector2 _mouvement)
    {
        Vector3 targetVelocity = mouvement;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, _mouvement, ref velocity, smoothTime);
    }

}
