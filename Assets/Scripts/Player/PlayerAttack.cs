using System.Collections;
using UnityEngine;

/// <summary>
///  Classe gérant les attaques du joueur
/// </summary>

public class PlayerAttack : Player
{

    public LayerMask enemyLayer;
    public float attackRadius;
    public GameObject projectile;
    private GameObject cacWeapons;
    private bool isCaC = true;

    protected override void Awake()
    {
        base.Awake();
        cacWeapons = GameObject.FindGameObjectWithTag("WeaponManager");
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            InstantiateProjectile();
        }
    }

 

   // void AttackCACMono()
    //{
      //  Collider2D enemyHit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);

        // do something

   // }

    void InstantiateProjectile()
    {
        GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
    }

    // Gizmo de Test
   




}

