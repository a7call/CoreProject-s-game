using UnityEngine;

public class LaserGun : Weapons, IShootableWeapon
{

    #region ScriptableObject
    public WeaponScriptableObject WeaponData
    {
        get
        {
            return LaserDatas;
        }
    }

    public CollingWeaponScriptableObject LaserDatas;
    #endregion

    #region Unity Mono
    protected override void Awake()
    {
        SetData();
        base.Awake();
    }

    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>(); 
        DisableLaser();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!OkToShoot)
        {
            DisableLaser();
            return;
        }

        if (canDisplay)
        {
            UpdateLaser();
        }

        StartShootingProcess();
    }
    #endregion

    #region Datas
    protected virtual void SetData()
    {
        enemyLayer = WeaponData.enemyLayer;
        image = WeaponData.image;
    }
    #endregion 

    #region Shoot Logic
    private LineRenderer lineRenderer;

    public bool OkToShoot { get; set; }
    void UpdateLaser()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, attackPoint.position);
        lineRenderer.SetPosition(1, transform.right * 20  + transform.position);

        Vector2 direction = transform.right*100;
        Debug.DrawRay((Vector2)transform.position, direction,Color.red);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction, Mathf.Infinity, enemyLayer);

        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(player.damage.Value);
            }
            lineRenderer.SetPosition(1, hit.point);
        }
           

    }

    void DisableLaser()
    {
        lineRenderer.SetPosition(1, Vector2.zero);
        lineRenderer.enabled = false;
        isAttacking = false;
        canDisplay = false;
    }

    public void StartShootingProcess()
    {
        if (IsAbleToShoot())
        {
            isAttacking = true;
            if (animator)
            {
                animator.SetTrigger("isAttacking");
            }
        }
        
    }
    bool canDisplay = false;
    void DisplayLaser()
    {
        canDisplay = true;
    }
    public bool IsAbleToShoot()
    {
        return OkToShoot && !isAttacking  && !PauseMenu.isGamePaused;
    }
    #endregion

}
