using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesProjectile : Projectile
{
    protected bool damageDone = false;
    private bool ReadyToShoot = false;
    [SerializeField] protected float ShootDelay;
    Vector3 directionTir;
    public float angleDecalage;

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



    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        base.OnTriggerEnter2D(collision);
        // Damage
    }

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
    void Update()
    {
        StartCoroutine(OkToShoot());

        if (ReadyToShoot == true)
        {

            StartCoroutine(destroy());
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionTir);

            Debug.DrawRay(transform.position, directionTir * 10, Color.red);

            if (hit.collider.CompareTag("Player") && !damageDone)
            {
                //take Damage
                Debug.Log("Damage");
                damageDone = true;

            }

        }
    }
}
