using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class PompeDistanceEnemy : DistanceWithWeapon
{
    [SerializeField] GameObject[] projectiles = null;
    [SerializeField] int angleTir = 0;
    [SerializeField] int NbRafale = 0;
    [SerializeField] int TimeIntervaleTir = 0;

    private AngleProjectile AngleProjectile;

    protected override void Start()
    {
        base.Start();
        GetProjectile();
    }

    // Voir Enemy.cs (héritage)
    protected override IEnumerator ShootCO()
    {
        
        for (int i = 0; i < NbRafale; i++)
        {

            StartCoroutine(ShootPump());
            yield return new WaitForSecondsRealtime(TimeIntervaleTir);
        }
    }

    private void GetProjectile()
    {
        foreach (GameObject projectile in projectiles)
        {
            AngleProjectile = projectile.GetComponent<AngleProjectile>();
        }
    }

    protected IEnumerator ShootPump()
    {
        
        float decalage = angleTir / (projectiles.Length - 1);
        AngleProjectile.angleDecalage = -decalage * (projectiles.Length + 1) / 2;

        //base.Shoot();
        for (int i = 0; i < projectiles.Length; i++)
        {
            AngleProjectile.angleDecalage = AngleProjectile.angleDecalage + decalage;
            GameObject myProjectile = Instantiate(projectiles[i], attackPoint.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
        }
        yield return new WaitForEndOfFrame();
    }
}
