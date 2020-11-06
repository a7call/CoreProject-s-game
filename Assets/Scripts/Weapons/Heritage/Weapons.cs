using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe mère des armes 
/// </summary>
public class Weapons : MonoBehaviour
{

    [HideInInspector]
    public int damage;
    [HideInInspector]
    public static bool isTotalDestructionModule;
    [HideInInspector]
    public static int damageMultiplier;
    [HideInInspector]
    protected bool damagealReadyMult;
    [HideInInspector]
    public LayerMask enemyLayer;
    protected bool readyToAttack;

    public Transform attackPoint;
    protected float attackRadius;
    protected float attackDelay;
    public bool isAttacking = false;
    Vector3 screenMousePos;
    Vector3 screenPlayerPos;

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
        attackPoint.position = new Vector2(transform.position.x + (screenMousePos - screenPlayerPos).normalized.x, transform.position.y + (screenMousePos - screenPlayerPos).normalized.y);
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

