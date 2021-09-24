using System.Collections;
using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
    public float Damage { get; protected set; }
    public LayerMask HitLayer { get; set; }
    public Vector3 Direction { get; protected set; }
    protected GameObject Source { get;  set; }
    public abstract void SetProjectileDatas(float damage, float dispersion, float projectileSpeed, LayerMask hitLayer, GameObject source, float timeAlive, Vector3 direction);
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
