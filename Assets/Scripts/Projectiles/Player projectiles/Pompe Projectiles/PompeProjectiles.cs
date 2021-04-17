
using UnityEngine;


public class PompeProjectiles : PlayerProjectiles
{
  
    protected Vector3 dirTir;
    public float angleDecalage;

    protected override void Launch()
    {
        directionTir = Quaternion.AngleAxis(angleDecalage, Vector3.forward) * transform.right;
        rb.velocity = directionTir * projectileSpeed;
    }

}
