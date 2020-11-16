using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles360 : Projectile
{
    //M'expliquer
    private float radius = 0f;
    private float coeffDir = 0f;
    //private float time = 10f;
    //private float timeRate = 1f;
    protected bool damageDone = false;
    private bool ReadyToShoot = false;
    [SerializeField] protected float ShootDelay;

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
    }

    // Update is called once per frame
    protected override void Update()
    {
        StartCoroutine(OkToShoot());

        if (ReadyToShoot == true)
        {

            StartCoroutine(destroy());

            RaycastHit2D hit = Physics2D.CircleCast(transform.position, UploadRadius(), dir) ;

            if (hit.collider.CompareTag("Player") && !damageDone)
            {
                //take Damage
                Debug.Log("Damage");
                damageDone = true;

            }

        }
        if (isTacticVisionModule && !AmmoSpeedAlreadyDown)
        {
            AmmoSpeedAlreadyDown = true;
            speed /= SpeedDiviser;
        }

    }

    private float UploadRadius()
    {
        radius = coeffDir * Time.deltaTime;
        return radius;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(gameObject);
    // Damage
    //}

    protected override void GetDirection()
    {
        dir = (target.position - transform.position).normalized;
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
