
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>


public class AngleProjectile : Projectile
{
   public float angleDecalage;
   Vector3 directionTir;

    protected override void Start()
    {
        base.Start();
        GetDirection();
        ConeShoot();

    }


    protected override void Lauch()
    {
        transform.Translate(directionTir * base.speed * Time.deltaTime);
    }

    protected void ConeShoot()
    {
        directionTir = Quaternion.AngleAxis(angleDecalage, Vector3.forward) * dir;
    }

}
