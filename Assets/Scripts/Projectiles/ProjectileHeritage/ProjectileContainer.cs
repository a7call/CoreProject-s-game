using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileContainer : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float MovementForce { get; set; }
    protected Vector3 Direction { get; set; }
    protected virtual void Awake()
    {
        GetReferences();
    }
    protected void GetReferences()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetProjectileContainerDatas(float damage, float dispersion, float projectileSpeed, LayerMask hitLayer, GameObject source, float timeAlive, Vector3 direction)
    {
        this.MovementForce = projectileSpeed + 150;
        this.Direction = direction;
        foreach (Transform transform in gameObject.transform)
        {
            transform.GetComponent<ProjectileInContainer>().SetProjectileDatas(damage, 0, 0, hitLayer, source, timeAlive, direction);
        }
        Launch(dispersion);
    }
    protected void Launch(float dispersion)
    {
        var directionTir = Quaternion.AngleAxis(dispersion, Vector3.forward) * Direction;
        rb.AddForce(directionTir.normalized * MovementForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}
