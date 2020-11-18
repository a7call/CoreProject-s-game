using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>
public class LaserDecalage : Projectile
{
    protected bool damageDone = false;
    private bool ReadyToShoot = false;
    [SerializeField] protected float ShootDelay;
    Vector3 directionTir;
    public float angleDecalage;
    [SerializeField] protected LayerMask HitLayer;
    

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
        ConeShoot();
       
    }

    protected void ConeShoot()
    {
        directionTir = Quaternion.AngleAxis(angleDecalage, Vector3.forward) * dir;
    }

    

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
     //   Destroy(gameObject);
        // Damage
    //}

    protected override void GetDirection()
    {
        base.GetDirection();
    }

    protected override void Lauch()
    {
        base.Lauch();
    }

    protected IEnumerator destroy()
    {
        yield return new WaitForSeconds(ActiveTime);
        Destroy(gameObject);
    }

    protected IEnumerator OkToShoot()
    {
        yield return new WaitForSeconds(ShootDelay);
        ReadyToShoot = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        StartCoroutine(OkToShoot());

        if (ReadyToShoot == true)
        {
            
            StartCoroutine(destroy());
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionTir, Mathf.Infinity, HitLayer);

            Debug.DrawRay(transform.position, directionTir * 10, Color.red);

            if (hit.collider != null)
            {
                playerHealth.TakeDamage(1);
            }

        }
    }
}






