using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacWeapons : Weapons
{
    [SerializeField]
    protected Transform attackPoint;
    Vector3 screenMousePos;
    Vector3 screenPlayerPos;

    protected virtual void GetAttackDirection()
    {

        screenMousePos = Input.mousePosition;
        screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        attackPoint.position = new Vector2(transform.position.x + (screenMousePos - screenPlayerPos).normalized.x, transform.position.y + (screenMousePos - screenPlayerPos).normalized.y);
    }
}
