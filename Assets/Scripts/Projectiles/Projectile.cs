using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    protected GameObject Source { get; private set; }

    protected Rigidbody2D rb;

    #region Stats
    public float Damage { get; private set; }
    public float MovementForce { get; private set; }
    public float Dispersion { get; private set; }
    public LayerMask HitLayer { get;  set; }
    public Vector3 Direction { get; private set; }
    protected virtual void Awake()
    {
        GetReferences();
    }
    protected void GetReferences()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected void Start()
    {
       // Launch(Dispersion);
    }

    #endregion
    public void SetProjectileDatas(float damage, float dispersion, float projectileSpeed, LayerMask hitLayer, GameObject source, float timeAlive, Vector3 direction)
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
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Mathf.Log(HitLayer.value, 2))
        {
            collision.GetComponent<Characters>().TakeDamage(Damage, Source);
        }
        gameObject.SetActive(false);
    }
    protected IEnumerator DetroyProjectileCo(float timeAlive)
    {
        yield return new WaitForSeconds(timeAlive);
        gameObject.SetActive(false);
    }

}
