using UnityEngine;
/// <summary>
/// Classe mère des armes 
/// </summary>
public class Weapons : MonoBehaviour
{
    protected GameObject playerGO;
    protected Player player;
    protected Animator animator;
    public Transform attackPoint;
    public bool isAttacking = false;

    // Offset de la postion de l'arme
    public float offSetX;
    public float topOffSetY;
    public float otherOffsetY;
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
        audioManagerEffect = FindObjectOfType<AudioManagerEffect>();
    }
    #endregion

    #region Sound

    //Sounds
    protected AudioManagerEffect audioManagerEffect;
   

    protected void PlayEffectSound(string SoundToPlay)
    {
        if (audioManagerEffect != null)
            audioManagerEffect.Play(SoundToPlay);
    }


    #endregion


    /* ANCIENNE METHODE GETDIRPROJ
    *  
    * protected virtual void GetDirProj()
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
    } */

    protected Vector3 dirProj;

}

