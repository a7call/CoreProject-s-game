using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceWeaponTest : MonoBehaviour
{
    public Weapon weaponData;
    public Transform attackPoint;
    public GameObject projectile;
    bool isAttacking;
    [HideInInspector]
    public bool OkToShoot;
    PlayerMouvement player;
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        player = transform.parent.parent.GetComponent<PlayerMouvement>();
        weaponData.Equip(player);
    }
    public void OnDisable()
    {
        weaponData.Unequip(player);
    }
    protected virtual void GetAttackDirection()
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition.z = Camera.main.nearClipPlane;      
        attackPoint.position = worldMousePosition;
    }

    private void Update()
    {
        GetAttackDirection();
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartShootingProcess();
        }
       
    }


    protected virtual IEnumerator Shoot()
    {
        //float decalage = Random.Range(-Dispersion, Dispersion);
        //Proj.Dispersion = decalage;
        //BulletInMag--;
        Instantiate(projectile, attackPoint.position, Quaternion.identity);
        float delaye = player.attackSpeed.Value;
        yield return new WaitForSeconds(delaye);
        isAttacking = false;
    }

    private bool IsAbleToShoot()
    {
        
        return OkToShoot && !isAttacking  && !PauseMenu.isGamePaused;
    }

    private void StartShootingProcess()
    {

            isAttacking = true;
            StartCoroutine(Shoot());
        
    }
}
