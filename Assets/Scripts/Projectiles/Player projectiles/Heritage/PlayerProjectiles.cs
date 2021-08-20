using UnityEngine;
public class PlayerProjectiles : MonoBehaviour
{
    protected Player Player { get; private set; }
    protected Rigidbody2D rb;


    #region Stats
    public float Damage { get; private set; }
    public float ProjectileSpeed { get; private set; }
    public float Dispersion { get; private set; }
    public LayerMask WeaponLayer { get; private set; }
    #endregion



    protected virtual void Awake()
    {        
        GetReferences();
    }
    protected void GetReferences()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        Launch(Dispersion);
    }

    #region Reférences
    public void SetProjectileDatas(float damage, float dispersion, float projectileSpeed, LayerMask WeaponLayer, Player player)
    {
        this.Damage = damage;
        this.Dispersion = dispersion;
        this.Player = player;
        this.ProjectileSpeed = projectileSpeed;
        this.WeaponLayer = WeaponLayer;
    }
   
    #endregion

   
    #region Move logic  
    protected virtual void Launch(float dispersion)
    {
        var directionTir = Quaternion.AngleAxis(dispersion, Vector3.forward) * transform.right;
        rb.velocity = directionTir.normalized * ProjectileSpeed;      
    }
    #endregion


    #region Collision logic

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(Damage, Player.gameObject);            
        }
         Destroy(gameObject);
    }
    #endregion

    #region Lost Code to keep

    //IntelligentAmoModule
    //private float angulSpeed = 200f;
    //private GameObject lockEnemy;
    //public void getNewDir(GameObject proj)
    //{
    //    if (InteligentAmmoModule.LockEnemy(proj) != null) lockEnemy = InteligentAmmoModule.LockEnemy(proj);
    //    if (lockEnemy != null)
    //    {
    //        if (lockEnemy == null) return;
    //        Vector2 direction = (lockEnemy.transform.position - proj.transform.position);
    //        direction.Normalize();
    //        float rotationAmount = Vector3.Cross(direction, (transform.up * directionTir.y + transform.right * directionTir.x)).z;
    //        rb.angularVelocity = -rotationAmount * angulSpeed;
    //        rb.velocity = (transform.up * directionTir.y + transform.right * directionTir.x) * projectileSpeed;
    //        angulSpeed += 2;
    //    }
    //    else
    //    {
    //        Launch();
    //    }


    //}
    #endregion

}
