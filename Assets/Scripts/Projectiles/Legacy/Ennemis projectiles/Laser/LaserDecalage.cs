using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>
public class LaserDecalage : AIProjectile
{
    //protected bool damageDone = false;
    //private bool ReadyToShoot = false;
    //[SerializeField] protected float ShootDelay;
    //Vector3 directionTir;
    //public float angleDecalage;
    //[SerializeField] protected LayerMask HitLayer;
    //[SerializeField] public float ActiveTime;


    //// Start is called before the first frame update
    //protected override void Start()
    //{
    //    target = GetComponentInParent<Enemy>().Target;
    //    SetMoveDirection();
    //    ConeShoot();
    //    StartCoroutine(OkToShoot());
        
    //}

    //protected void ConeShoot()
    //{
    //    directionTir = Quaternion.AngleAxis(angleDecalage, Vector3.forward) * dir;
    //}

    

    //protected IEnumerator destroy()
    //{
    //    yield return new WaitForSeconds(ActiveTime);
    //    Destroy(gameObject);
    //}

    //protected IEnumerator OkToShoot()
    //{
    //    yield return new WaitForSeconds(ShootDelay);
    //    ReadyToShoot = true;
    //}

    //// Update is called once per frame
    //protected override void FixedUpdate()
    //{
        

    //    if (ReadyToShoot == true)
    //    {
            
    //        StartCoroutine(destroy());
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionTir, Mathf.Infinity, HitLayer);
    //        Debug.DrawRay(transform.position, directionTir * 10, Color.red);

    //        if (hit.collider != null)
    //        {
    //            hit.collider.gameObject.GetComponent<Player>().TakeDamage(1);
    //        }

    //    }
    //}
}






