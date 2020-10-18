using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>
public class Laser : Projectile
{
    protected bool damageDone = false;
    private bool ReadyToShoot = false;
    [SerializeField] protected float ShootDelay;

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(OkToShoot());

        if (ReadyToShoot == true)
        {

            StartCoroutine(destroy());
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);

            Debug.DrawRay(transform.position, dir * 10, Color.red);

            if (hit.collider.CompareTag("Player") && !damageDone)
            {
                //take Damage
                Debug.Log("Damage");
                damageDone = true;
                
            }
            
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(gameObject);
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
            //DistanceLaser.State.Chasing;
        Destroy(gameObject);
    }

    protected IEnumerator OkToShoot()
    {
        yield return new WaitForSeconds(ShootDelay);
        ReadyToShoot = true;
    }
}






