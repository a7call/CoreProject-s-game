using System.Collections;
using UnityEngine;


public class ProjectileInContainer : Projectile
{
    public override void SetProjectileDatas(float damage, float dispersion, float projectileSpeed, LayerMask hitLayer, GameObject source, float timeAlive, Vector3 direction)
    {
        this.Damage = damage;
        this.Source = source;
        this.HitLayer = hitLayer;
        this.Direction = direction;
    }
}
