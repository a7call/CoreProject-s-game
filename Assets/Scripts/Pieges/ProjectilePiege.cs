using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePiege : MonoBehaviour
{
    #region Variables
    protected Player player;
    public Vector3 directionTir;
    private Rigidbody2D rb;
    [SerializeField] private float projectileSpeed;

    #endregion Variables

    #region Fonctionnement
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void Launch()
    {
        //transform.Translate(directionTir * projectileSpeed * Time.deltaTime);
        rb.velocity = directionTir * projectileSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(1);
            Destroy(gameObject);
        }
        //if (collision.CompareTag("Enemy"))
        //{
        //    collision.GetComponent<Enemy>().TakeDamage(1);
        //    Destroy(gameObject);
        //}

        if (collision.gameObject.layer == 10) Destroy(gameObject);

    }
    #endregion Fonctionnement
}
