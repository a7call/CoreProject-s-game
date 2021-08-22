using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouleEnergiePiege : HeritagePiege
{
    #region Variables
    private bool isReadyToShoot;
    [SerializeField] private float damage;
    [SerializeField] private float ShootDelay;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPoint;

    #endregion Variables
    private void Start()
    {
        isReadyToShoot = true;
    }

    private void Update()
    {
        if (isReadyToShoot) StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        isReadyToShoot = false;
        Shoot();
        yield return new WaitForSeconds(ShootDelay);
        isReadyToShoot = true;
    }

    private void Shoot()
    {
        ProjectilePiege scriptProj = projectile.GetComponent<ProjectilePiege>();
        scriptProj.directionTir = (attackPoint.transform.position - transform.position).normalized;
        Instantiate(projectile, attackPoint.position, transform.rotation);
    }

}
