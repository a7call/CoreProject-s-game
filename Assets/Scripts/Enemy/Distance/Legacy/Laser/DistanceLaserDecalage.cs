//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
///// <summary>
///// Classe héritière de Distance.cs
///// Elle contient les fonctions de la classe mère
///// </summary>
//public class DistanceLaserDecalage : DistanceNoGun
//{
//    public override IEnumerator InstantiateProjectileCO()
//    {
//        throw new System.NotImplementedException();
//    }
//}
////{
////    Vector3 directionTir;

////    [SerializeField] int projectiles = 10;
////    [SerializeField] int angleTir = 360;
////    [SerializeField] int offset = 0;
////    float decalage = 0f;

////    void Start()
////    {

////        // Set data
////        SetData();
////        SetMaxHealth();

////    }

//// Voir Enemy.cs (héritage)
////protected override void Shoot()
////{
////    if (isShootingLasers)
////    { 
////        for(int i=0; i < projectiles ; i++)
////        {
////            float angleDecalage = offset - decalage * (i) ;
////            directionTir = Quaternion.AngleAxis(angleDecalage, Vector3.forward) * dir;
////            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionTir, Mathf.Infinity);
////            Debug.DrawRay(transform.position, directionTir * 10, Color.red);
////            foreach (RaycastHit2D hit in hits)
////            {
////                if (hit.transform.gameObject.CompareTag("Player"))
////                {
////                    Player player = hit.transform.gameObject.GetComponent<Player>();
////                    player.TakeDamage(damage);
////                }
////            }
////        } 
////    }

////}


////protected override IEnumerator ShootCO()
////{
////    if (!isShootingLasers )
////    {

////        decalage = angleTir / (projectiles);  
////        dir = (targetSetter.target.position - transform.position).normalized;
////        yield return new WaitForSeconds(timeBeforeShoot);
////        isShootingLasers = true;
////        yield return new WaitForSeconds(durationOfShoot);
////        isShootingLasers = false;
////    }
////}

////protected override IEnumerator CanShoot()
////{
////    if (isShooting && isreadyToAttack && !isPerturbateurIEM)
////    {   
////        isreadyToAttack = false;
////        StartCoroutine(ShootCO());
////        yield return new WaitForSeconds(restTime);
////        isreadyToAttack = true;
////    }
////}




