
using UnityEngine;


public class PompeProjectiles : PlayerProjectiles
{
  
    protected Vector3 dirTir;
    public float angleDecalage;

    protected override void Awake()
    {
        base.Awake();
        Cone();
    }

    protected override void Launch()
    {
        transform.Translate(dirTir * projectileSpeed * Time.deltaTime);
    }
    protected void Cone()
    {
        dirTir = Quaternion.AngleAxis(angleDecalage, Vector3.forward) * directionTir;
    }

}
