using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PompeProjectiles : PlayerProjectiles
{
  
    protected Vector3 directionTir;
    public float angleDecalage;

    protected override void Awake()
    {
        base.Awake();
        ConeShoot();
    }

    protected override void Launch()
    {
        transform.Translate(directionTir * speed * Time.deltaTime);
    }

    protected void ConeShoot()
    {
        directionTir = Quaternion.AngleAxis(angleDecalage, Vector3.forward) * dir;
    }
}
