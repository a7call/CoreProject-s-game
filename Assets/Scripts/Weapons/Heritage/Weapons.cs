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

    // Offset de la postion de l'arme
    public Vector3 OffPositionArme;
    [SerializeField] private Vector3 topOffSet;
    [SerializeField] private Vector3 otherOffset;


    protected GameObject playerGO;
    protected Player player;

    public float RangeMiniChangementTir = 5;
    [SerializeField] public float RangeChangementTir;

    // Animator
    protected Animator animator;

    // Sprite Renderer
    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        this.enabled = false;
        animator = gameObject.GetComponent<Animator>();
    }

    protected void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    protected virtual void OnEnable()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<Player>();
    }
    // recupère en temps réel la position de la souris et associe cette position au point d'attaque du Player
    protected virtual void GetAttackDirection()
    {
        // position de la souris sur l'écran 
        screenMousePos = Input.mousePosition;
        // position du player en pixel sur l'écran 
        screenPlayerPos = Camera.main.WorldToScreenPoint(playerGO.transform.position);
        // position du point d'attaque
        screenArmePos = Camera.main.WorldToScreenPoint(attackPoint.transform.position);

        posSouris = new Vector3((screenMousePos - screenArmePos).x , (screenMousePos - screenArmePos).y).normalized;
    }

    protected virtual void ChangeLayer()
    {
        // Pour récuperer la position de la souris
        GetAttackDirection();

        // On change de layer pour des lorsque le joueur regarde en haut
        // C'est-à-dire, lorsque y > 0 et x < cos(45)
        float angle45 = Mathf.Sqrt(2) / 2;

        if (posSouris.y > 0 && Mathf.Abs(posSouris.x) <= angle45)
        {
            OffPositionArme = topOffSet;
            spriteRenderer.sortingOrder = 0;
        }
        else
        {
            OffPositionArme = otherOffset;
            spriteRenderer.sortingOrder = 2;
        }

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
        ChangeLayer();
        if (isTotalDestructionModule && !damagealReadyMult)
        {
            damagealReadyMult = true;
            damage *= damageMultiplier;
        }
    }

}

