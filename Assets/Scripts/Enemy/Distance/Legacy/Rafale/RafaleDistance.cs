using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
//public class RafaleDistance : UnDeadMecaDistance
//{
//    //[SerializeField] protected RafaleDistanceData RafaleDistanceData;

//    protected float timeIntervale;
//    protected int nbTir;

//    protected int n = 0; //compteur pour le while

//    protected override void Awake()
//    {
//        base.Awake();
//        SetData();
//        SetMaxHealth();
//    }
//    protected override void SetData()
//    {
//        base.SetData();
//        timeIntervale = DistanceData.timeIntervale;
//        nbTir = DistanceData.nbTir;
//    }

//    // Voir Enemy.cs (héritage)
  
//    protected virtual  IEnumerator intervalleTir()
//    { 
//        while (n < nbTir )
//        {
//            isAttacking = true;
//            yield return StartCoroutine(base.InstantiateProjectileCO());
//            yield return new WaitForSeconds(timeIntervale);
//            n++;
//        }
//        n = 0;
//    }
//}
