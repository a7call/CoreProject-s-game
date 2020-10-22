using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe mère des armes 
/// </summary>
public class Weapons : MonoBehaviour
{

    protected int damage;
    protected LayerMask enemyLayer;
    protected bool readyToAttack;

    [SerializeField]
    protected Transform attackPoint;
    protected float attackRadius;
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


  

}

