using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour
{
    private AngleProjectile AngleProjectile;
    [SerializeField] GameObject[] projectiles;

    private float timeShoot = 6f;
    private int angleTir = 360;

    private Transform playerPos;

    void Start()
    {
        playerPos = FindObjectOfType<Player>().transform;
        GetProjectile();
        Die();
        StartCoroutine(DestroyObject());
    }

    private void Die()
    {
        AngleProjectile.target = playerPos;
        float decalage = angleTir / (projectiles.Length - 1);
        AngleProjectile.angleDecalage = -decalage * (projectiles.Length + 1) / 2;

        //base.Shoot();
        for (int i = 0; i < projectiles.Length; i++)
        {
            AngleProjectile.angleDecalage = AngleProjectile.angleDecalage + decalage;
            GameObject myProjectile = GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
        }
    }
    private void GetProjectile()
    {
        foreach (GameObject projectile in projectiles)
        {
            AngleProjectile = projectile.GetComponent<AngleProjectile>();
        }
    }


    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(timeShoot);
        Destroy(gameObject);
    }
}
