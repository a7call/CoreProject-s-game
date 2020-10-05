using System.Collections;
using UnityEngine;

/// <summary>
///  Classe gérant les attaques du joueur
/// </summary>

public class PlayerAttack : MonoBehaviour
{

    public PlayerScriptableObjectScript playerData;
    public Animator animator;
    public LayerMask enemyLayer;
    public float attackRadius;
    public GameObject projectil;
    private GameObject cacWeapons;
    private bool isCaC = true;

    private void Awake()
    {
        cacWeapons = GameObject.FindGameObjectWithTag("WeaponManager");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SwitchAttackMode();
        }
        if (!isCaC)
        {
            GetAttackDirection();
        }
    }

 

   // void AttackCACMono()
    //{
      //  Collider2D enemyHit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);

        // do something

   // }

    void InstantiateProjectile()
    {
        GameObject.Instantiate(projectil);
    }

    // Gizmo de Test
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,attackPoint.position);
    }


    private void SwitchAttackMode()
    {
        if (isCaC)
        {
            cacWeapons.SetActive(false);
            isCaC = false;
        }
        else
        {
            cacWeapons.SetActive(true);
            isCaC = true;
        }
        
    }

    [SerializeField]
    protected Transform attackPoint;
    Vector3 screenMousePos;
    Vector3 screenPlayerPos;

    // recupère en temps réel la position de la souris et associe cette position au point d'attaque du Player
    protected virtual void GetAttackDirection()
    {

        // position de la souris sur l'écran 
        screenMousePos = Input.mousePosition;
        // position du player en pixel sur l'écran 
        screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        // position du point d'attaque 
        attackPoint.position = new Vector2(transform.position.x + (screenMousePos - screenPlayerPos).normalized.x, transform.position.y + (screenMousePos - screenPlayerPos).normalized.y);
    }




}

