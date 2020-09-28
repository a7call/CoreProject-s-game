using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs 
/// Elle contient une fonction permettant de faire pop des mobs à son contact avec un autre collider
/// </summary>
public class EggProjectile : Projectile
{
   [SerializeField] protected GameObject mobs;

    protected void Start()
    {
        GetDirection();
       // Invoke("PopMobs", 3f);
    }

    protected void Update()
    {
        Lauch();
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Instantiate(mobs, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // Damage
    }

    protected override void GetDirection()
    {
        base.GetDirection();
    }

    protected override void Lauch()
    {
        base.Lauch();
    }

    // Pop mob if touch nothing 
    protected void PopMobs()
    {
        GameObject.Instantiate(mobs, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
