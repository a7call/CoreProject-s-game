using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs 
/// Elle contient une fonction permettant de faire pop des mobs à son contact avec un autre collider
/// </summary>
public class EggProjectile : Projectile
{
   [SerializeField] protected GameObject mobs;

    protected override void Start()
    {
        base.Start();
        GetDirection();
    }

  
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PopMobs();
        }
        base.OnTriggerEnter2D(collision);

    }

    // Pop mob if touch nothing 
    protected void PopMobs()
    {
        GameObject mob = GameObject.Instantiate(mobs, transform.position, Quaternion.identity);
        mob.GetComponent<Enemy>().isInvokedInBossRoom = true;
    }
}
