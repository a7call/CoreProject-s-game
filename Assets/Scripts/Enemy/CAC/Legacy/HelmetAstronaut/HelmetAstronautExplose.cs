//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class HelmetAstronautExplose : Cac
//{
//    [SerializeField] private float timeToExplode = 1f;

//    protected override void Update()
//    {
//    }

//    IEnumerator TriggerExplosion()
//    {
//       yield return new WaitForSeconds(timeToExplode);
//       AIMouvement.ShouldMove = false;
//       Die();
//    }

//    // Similar to ApplyDamage but for Die animation event
//    void OnDeathDamage()
//    {
//        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, AttackRadius, HitLayers);

//        foreach (Collider2D h in hits)
//        {

//            if (h.CompareTag("Player"))
//            {
//                h.GetComponent<Player>().TakeDamage(1);
//            }

//        }
//    }
//}
