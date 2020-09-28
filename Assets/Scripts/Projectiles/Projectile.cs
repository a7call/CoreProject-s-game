using UnityEngine;
using UnityEngine.WSA;

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
    public float speed;
    // cible des projectiles (Player)
    private Transform target;
    // direction (en fonction de la place de la cible)
    protected Vector3 dir;
    // distance entre le player et le projectile
    protected float distance;
    
    protected virtual void Awake()
    {
        //Get player reference;
       target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // recupère la direction à prendre
    protected virtual void GetDirection()
    {
        dir = (target.position - transform.position).normalized;
    }

    //envoie le projectile
    protected virtual void Lauch()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    protected virtual void CalculDistance()
    {
        distance = Vector3.Distance(target.position, transform.position);
    }

}

