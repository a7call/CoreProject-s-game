using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe mère des projectiles
/// Contient la référence au joueur (target)
/// Une fonction pour avoir la direction que le projectiles doit prendre 
/// Une fonction mettant en mouvement le projectile
/// Une fonction de calcule de distance entre le joueur et le projectile
/// </summary>
public class Projectile : MonoBehaviour
{
    // vitesse des projectiles
    public float projectileSpeed;
    
    // direction (en fonction de la place de la cible)
    [HideInInspector]
    public Vector3 dir;

    protected Player player;

    [HideInInspector]
    public float dispersion=0;

    protected virtual void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    protected virtual void FixedUpdate()
    {
        Lauch();
    }

    // recupère la direction à prendre
    public void SetMoveDirection(Vector3 projDirection)
    {
        dir = Quaternion.AngleAxis(dispersion, Vector3.forward)*(projDirection).normalized;
    }
    
    //envoie le projectile
    protected virtual void Lauch()
    {
        transform.Translate(dir * projectileSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            player.TakeDamage(1);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 10) Destroy(gameObject);
    }
}

