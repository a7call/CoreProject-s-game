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
    public Transform target;
    public bool isConverted;
    // direction (en fonction de la place de la cible)
    [HideInInspector]
    public Vector3 dir;
    // distance entre le player et le projectile
    protected float distance;
    protected Player player;

    [HideInInspector]
    public float dispersion=0;

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
        player = FindObjectOfType<Player>();
    }

    protected virtual void Start()
    {
        if (GetComponentInParent<Enemy>())
        {
            target = GetComponentInParent<Enemy>().target;
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        transform.parent = null;
       
    }
    protected virtual void FixedUpdate()
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
    //protected void ConeShoot()
    //{
    //    directionTir = Quaternion.AngleAxis(dispersion, Vector3.forward) * dir;
    //}

    // recupère la direction à prendre
    protected virtual void GetDirection()
    {
        dir = Quaternion.AngleAxis(dispersion, Vector3.forward)*(target.position - transform.position).normalized;
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
            player.TakeDamage(1);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy") &&  isConverted)
        {
            collision.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);
        }
        
        if (collision.gameObject.layer == 10) Destroy(gameObject);




    }
}

