using System.Collections;
using System.Collections.Generic;
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
    public bool isDisabled;
    // cible des projectiles (Player)
    protected Transform target;
    // direction (en fonction de la place de la cible)
    [HideInInspector]
    public Vector3 dir;
    // distance entre le player et le projectile
    protected float distance;
    protected PlayerHealth playerHealth;

    //TacticVisionModule
    [HideInInspector]
    protected bool AmmoSpeedAlreadyDown = false;
    [HideInInspector]
    public static bool isTacticVisionModule;
    [HideInInspector]
    public static float SpeedDiviser;


    GameObject[] enemies;
    protected virtual void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    protected virtual void Start()
    {
        target = GetComponentInParent<Enemy>().target;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        transform.parent = null;
        foreach (Transform child in target)
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
    }
    protected virtual void Update()
    {
        if (!isDisabled)
        {
            Lauch();
        }

        if (isTacticVisionModule && !AmmoSpeedAlreadyDown)
        {
            AmmoSpeedAlreadyDown = true;
            speed /= SpeedDiviser;
        }
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

    // Calcul la distance à laquelle se situe le projectile du joueur
    protected virtual void CalculDistance()
    {
        distance = Vector3.Distance(target.position, transform.position);
        
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            playerHealth.TakeDamage(1);
        }
        if(!collision.CompareTag("DontDestroy")) Destroy(gameObject);
    }
}

