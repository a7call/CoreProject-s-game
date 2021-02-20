using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe mère des armes 
/// </summary>
public class Weapons : MonoBehaviour
{

    [HideInInspector]
    public float damage;
    [HideInInspector]
    public static bool isTotalDestructionModule;
    [HideInInspector]
    public static float damageMultiplier;
    [HideInInspector]
    protected bool damagealReadyMult;
    
    public LayerMask enemyLayer;
    protected bool readyToAttack;

    public Transform attackPoint;
    protected float attackRadius;
    protected float attackDelay;
    [HideInInspector]
    public bool isAttacking = false;
    protected Vector3 screenMousePos;
    protected Vector3 screenPlayerPos;
    public Vector3 posOfPoint;

    public Vector3 OffPositionArme;

    protected virtual void Awake()
    {
        this.enabled = false;
    }
    // recupère en temps réel la position de la souris et associe cette position au point d'attaque du Player
    protected virtual void GetAttackDirection()
    {

        // position de la souris sur l'écran 
        screenMousePos = Input.mousePosition;
        // position du player en pixel sur l'écran 
        screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        // position du point d'attaque 

        posOfPoint = new Vector3(transform.position.x + (screenMousePos - screenPlayerPos).normalized.x, transform.position.y + (screenMousePos - screenPlayerPos).normalized.y);
        attackPoint.position = posOfPoint;
    }

    protected virtual void Update()
    {
        if (isTotalDestructionModule && !damagealReadyMult)
        {
            damagealReadyMult = true;
            damage *= damageMultiplier;
        }
    }

}

