using Edgar.Unity.Examples.PC2D.Example;
using UnityEngine;
/// <summary>
/// Classe mère des armes 
/// </summary>
[RequireComponent(typeof(AudioSource))]
public abstract class Weapons : MonoBehaviour
{

    protected AudioSource audioSource { get; private set; }
    protected string ShootAudioName { get; set; }
    protected string ReloadAudioName { get; set; }

    protected GameObject playerGO;
    protected Player player;
    protected Animator animator;
    public Transform attackPoint;
    public bool isAttacking = false;

    protected float screenShakeMagnitude;
    protected float screenShakeTime;

    // Offset de la postion de l'arme
    public float offSetX;
    public float offSetY;

    [HideInInspector]
    public Vector3 attackPointPos;

   

    #region Module
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public static bool isTotalDestructionModule;
    [HideInInspector]
    public static float damageMultiplier;
    [HideInInspector]
    protected bool damagealReadyMult;
    #endregion


    #region Data variable
    public LayerMask enemyLayer;
    protected float attackDelay;
    #endregion


    protected virtual void Awake()
    {
        this.enabled = false;
        GetReferences();
    }
    protected virtual void Update()
    {
    }

    protected virtual void OnEnable()
    { 
        GetComponent<SpriteRenderer>().flipY = false;
        GetComponent<SpriteRenderer>().flipX = false;
        isAttacking = false;

    }

    public Sprite image { get;  set; }
    private void OnDisable()
    {
        GetComponent<SpriteRenderer>().sprite = image;
        
    }

    #region References 
    protected virtual void GetReferences()
    {
        animator = gameObject.GetComponent<Animator>();
        playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<Player>();
        attackPointPos = attackPoint.transform.localPosition;
        audioSource = GetComponent<AudioSource>();
    }
    #endregion

}

