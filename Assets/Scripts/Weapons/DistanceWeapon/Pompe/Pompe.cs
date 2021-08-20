using System.Collections;
using UnityEngine;

public class Pompe : ShootableWeapon
{

    private int numberOfProj = 3;
    private int angleTir = 10;

    protected override IEnumerator Shooting()
    {
        float decalage = angleTir / (numberOfProj - 1);
        var startDecalageAngle= -decalage * (numberOfProj + 1) / 2;
        
        for (int i = 0; i < numberOfProj; i++)
        {
            startDecalageAngle += decalage;
            ProjectileSetUp(startDecalageAngle);
            BulletInMag--;
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;

    }
}
