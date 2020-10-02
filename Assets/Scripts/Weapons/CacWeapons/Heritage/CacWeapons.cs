using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Weapons.cs et mère de toutes les armes CAC.
/// Elle contient une fonction permettant de recupérer la direction de l'attaque
/// </summary>
public class CacWeapons : Weapons
{
    [SerializeField]
    protected Transform attackPoint;
    Vector3 screenMousePos;
    Vector3 screenPlayerPos;

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
