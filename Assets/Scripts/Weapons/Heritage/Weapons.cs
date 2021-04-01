using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe mère des armes 
/// </summary>
public class Weapons : MonoBehaviour
{

    [HideInInspector]
    public float damage;
    [HideInInspector]
    public static bool isTotalDestructionModule;
    [HideInInspector]
    public static float damageMultiplier;
    [HideInInspector]
    protected bool damagealReadyMult;
    
    public LayerMask enemyLayer;
    protected bool readyToAttack;

    public Transform attackPoint;
    protected float attackRadius;
    public float attackDelay;
    [HideInInspector]
    public bool isAttacking = false;
    protected Vector3 screenMousePos;
    protected Vector3 screenPlayerPos;
    protected Vector3 screenArmePos;
    public Vector3 posSouris;

    public Vector3 OffPositionArme;

    protected GameObject player;

    public float RangeMiniChangementTir = 5;
    [SerializeField] public float RangeChangementTir;

    // Animator
    protected Animator animator;


    protected virtual void Awake()
    {
        this.enabled = false;
        animator = gameObject.GetComponent<Animator>();
    }

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // recupère en temps réel la position de la souris et associe cette position au point d'attaque du Player
    protected virtual void GetAttackDirection()
    {

        // position de la souris sur l'écran 
        screenMousePos = Input.mousePosition;
        // position du player en pixel sur l'écran 
        screenPlayerPos = Camera.main.WorldToScreenPoint(player.transform.position);
        // position du point d'attaque
        screenArmePos = Camera.main.WorldToScreenPoint(attackPoint.transform.position);

        posSouris = new Vector3((screenMousePos - screenArmePos).x , (screenMousePos - screenArmePos).y);
    }

    protected Vector3 dirProj;

    protected virtual void GetDirProj()
    {
        GetAttackDirection();

        float distSP = new Vector3((screenMousePos - screenPlayerPos).x , (screenMousePos - screenPlayerPos).y ).magnitude;

        if (distSP < RangeMiniChangementTir)
        {
            dirProj = new Vector3(attackPoint.transform.position.x - transform.position.x, attackPoint.transform.position.y - transform.position.y);

        }
        else if (distSP < RangeChangementTir && distSP > RangeMiniChangementTir)
        {
            dirProj = new Vector3((screenMousePos - screenPlayerPos).x - player.transform.position.x, (screenMousePos - screenPlayerPos).y - player.transform.position.y).normalized;

        }
        else
        {

            dirProj = (posSouris).normalized;
        }
    }

protected virtual void Update()
    {
        if (isTotalDestructionModule && !damagealReadyMult)
        {
            damagealReadyMult = true;
            damage *= damageMultiplier;
        }
    }

}

