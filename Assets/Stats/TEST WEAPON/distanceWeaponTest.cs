using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceWeaponTest : MonoBehaviour
{
   public Weapon weaponData;
    public Transform attackPoint; 


    protected virtual void GetAttackDirection()
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition.z = Camera.main.nearClipPlane;      
        attackPoint.position = worldMousePosition;
    }

    private void Update()
    {
        GetAttackDirection();
    }
}
