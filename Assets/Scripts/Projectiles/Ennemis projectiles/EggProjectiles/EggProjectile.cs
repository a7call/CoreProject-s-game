using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs 
/// Elle contient une fonction permettant de faire pop des mobs à son contact avec un autre collider
/// </summary>
public class EggProjectile : Projectile
{
   [SerializeField] protected GameObject mobs;
    private GameObject[] enemies;

    protected override void Start()
    {
        if (GetComponentInParent<Enemy>())
        {
            target = GetComponentInParent<Enemy>().Target;
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (Transform child in player.transform)
        {
            if (child.GetComponent<BoxCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(child.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
        foreach (GameObject enemy in enemies)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

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
        mob.transform.parent = gameObject.transform.parent;
    }
}
