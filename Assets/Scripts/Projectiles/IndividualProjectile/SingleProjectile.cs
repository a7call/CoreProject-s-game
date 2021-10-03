using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SingleProjectile : Projectile
{
    protected Rigidbody2D rb;

    #region Stats
    public float MovementForce { get; private set; }
    public float Dispersion { get; private set; }
    protected virtual void Awake()
    {
        GetReferences();
    }
    protected void GetReferences()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    #endregion
    public override void SetProjectileDatas(float damage, float dispersion, float projectileSpeed, LayerMask hitLayer, GameObject source, float timeAlive, Vector3 direction)
    {
        this.Damage = damage;
        this.Dispersion = dispersion;
        this.Source = source;
        this.MovementForce = projectileSpeed;
        this.HitLayer = hitLayer;
        this.Direction = direction;
        StartCoroutine(DetroyProjectileCo(timeAlive));
        Launch(Dispersion);
    }
    protected void Launch(float dispersion)
    {
        var directionTir = Quaternion.AngleAxis(dispersion, Vector3.forward) * Direction;
        rb.AddForce(directionTir.normalized * MovementForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
   
}
