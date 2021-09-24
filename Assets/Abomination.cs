using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.Utils;

public class Abomination : DistanceNoGun
{
    public int numberOfProj;
    public int numberOfCircles;
    public int numberOfArrow;

    public GameObject Arrow;
    public GameObject Smoke;
    protected override void Start()
    {
        base.Start();
        PoolManager.GetInstance().CreatePool(Arrow, 8);
        PoolManager.GetInstance().CreatePool(Smoke, 12);
    }
    public override IEnumerator InstantiateProjectileCO()
    {
        var dir = (Target.transform.position - transform.position).normalized;
        var currentAttackPoint = attackPoint.position;
        ArrowShot(currentAttackPoint);
        StartCoroutine(CircularShot(dir, currentAttackPoint));
        yield return null;
    }

    public void ArrowShot(Vector3 attackPointPos)
    {
        float rangeOfAngles = 75;
        var initAngle = 0;

        for (int j = -numberOfArrow ; j <= numberOfArrow; j++)
        {
            float Dispersion = initAngle - j * rangeOfAngles / numberOfArrow;
            var initialDirection = (attackPointPos - transform.position).normalized;
            var arrowDirection = Quaternion.AngleAxis(Dispersion, Vector3.forward) * initialDirection;

            float angle = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;
            if(attackPoint.position != null)      
            {
                StartCoroutine(SmokeEffect(arrowDirection, attackPointPos));
                GameObject bul = PoolManager.GetInstance().ReuseObject(Arrow, attackPointPos, Quaternion.AngleAxis(angle, Vector3.forward));
                bul.GetComponent<ProjectileContainer>().SetProjectileContainerDatas(Damage, Dispersion, ProjetileSpeed, HitLayer, this.gameObject, 10, initialDirection);
            }     
        }
    }

    IEnumerator SmokeEffect(Vector3 dir, Vector3 attackPoint)
    {
        var currentNumberOfSmoke = 0;
        var distanceBetweenPoints = 0;
        while (currentNumberOfSmoke < 3)
        {
            currentNumberOfSmoke++;
            PoolManager.GetInstance().ReuseObject(Smoke, attackPoint + dir * 2*distanceBetweenPoints, Quaternion.identity);
            distanceBetweenPoints++;
            yield return new WaitForSeconds(0.25f);
        }
    }

    public IEnumerator CircularShot(Vector3 dir, Vector3 attackPointPos)
    {
        
        for (int i = 0; i < numberOfCircles; i++)
        {
            float rangeOfAngles = 90;
            var initAngle = 0;
            for (int j = - numberOfProj; j <= numberOfProj; j++)
            {
                float Dispersion = initAngle - j * rangeOfAngles / numberOfProj;
                GameObject bul = PoolManager.GetInstance().ReuseObject(Projetile, attackPointPos, Quaternion.identity);
                bul.GetComponent<SingleProjectile>().SetProjectileDatas(Damage, Dispersion, ProjetileSpeed, HitLayer, this.gameObject, 10, dir);
            }
            yield return new WaitForSeconds(0.1f);
        }
       
    }

}
