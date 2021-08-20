using UnityEngine;
/// <summary>
/// Classe mère des armes 
/// </summary>
public abstract class Weapons : MonoBehaviour
{
    protected string ShootAudioName { get; set; }
    protected string ReloadAudioName { get; set; }

    protected float screenShakeMagnitude;
    protected float screenShakeTime;

    protected Player player { get; private set; }
    protected Animator animator { get; private set; }
  

    // Offset de la postion de l'arme
    public float offSetX;
    public float offSetY;

    [HideInInspector]
    public Vector3 attackPointPos;
    public Transform attackPoint;
    public bool isAttacking = false;

    protected float damage;

    public LayerMask enemyLayer { get; protected set; }
    protected float attackDelay;
    public Sprite image { get; set; }

    #region Unity Mono
    protected virtual void Awake()
    {
        SetData();
        GetReferences();
        this.enabled = false;  
    }

    protected virtual void Start()
    {
        SetStatDatasAndInitialization();
    }

    private void OnDisable()
    {
        ResetWeaponState();
    }

    protected virtual void ResetWeaponState()
    {
        isAttacking = false;
        GetComponent<SpriteRenderer>().flipY = false;
        GetComponent<SpriteRenderer>().flipX = false;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    #endregion

    #region References 
    protected virtual void GetReferences()
    {
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        attackPointPos = attackPoint.transform.localPosition;
    }

    protected abstract void SetData();

    protected abstract void SetStatDatasAndInitialization();
    #endregion

}

