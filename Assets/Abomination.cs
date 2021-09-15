using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination : DistanceNoGun
{
    public int numberOfProj;
    public int numberOfArrow;
    public GameObject Arrow;
    public int numberOfCircles;
    protected override void Start()
    {
        base.Start();
        PoolManager.GetInstance().CreatePool(Arrow, 8);
    }
    public override IEnumerator InstantiateProjectileCO()
    {
        var dir = (Target.transform.position - transform.position).normalized;
        ArrowShot(dir);
        StartCoroutine(CircularShot(dir));
        yield return null;
    }

    public void ArrowShot(Vector3 dir)
    {

        float maxAngle = 90;
        float rangeOfAngles = 2 * maxAngle;
        for (int j = 0; j < numberOfArrow; j++)
        {
            float Dispersion = maxAngle;
            maxAngle = maxAngle -  rangeOfAngles / numberOfArrow;
            var directionTir = Quaternion.AngleAxis(Dispersion, Vector3.forward) * dir;
            float angle = Mathf.Atan2(directionTir.y, directionTir.x) * Mathf.Rad2Deg;
            if(attackPoint.position != null)
            {
                GameObject bul = PoolManager.GetInstance().ReuseObject(Arrow, attackPoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
                bul.GetComponent<ProjectileContainer>().SetProjectileContainerDatas(Damage, Dispersion, ProjetileSpeed, HitLayer, this.gameObject, 10, dir);
            }     
        }
    }

    public IEnumerator CircularShot(Vector3 dir)
    {
        
        for (int i = 0; i < numberOfCircles; i++)
        {
            float maxAngle = 90;
            for (int j = 0; j < numberOfProj; j++)
            {
                float Dispersion = maxAngle;
                maxAngle = maxAngle - 180 / numberOfProj;
                GameObject bul = PoolManager.GetInstance().ReuseObject(Projetile, attackPoint.position, Quaternion.identity);
                bul.GetComponent<SingleProjectile>().SetProjectileDatas(Damage, Dispersion, ProjetileSpeed, HitLayer, this.gameObject, 10, dir);
            }
            yield return new WaitForSeconds(0.1f);
        }
       
    }

}
