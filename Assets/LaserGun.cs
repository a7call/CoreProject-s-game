using UnityEngine;
using Wanderer.Utils;

public class LaserGun : Weapons, IShootableWeapon
{

    #region ScriptableObject
    public WeaponScriptableObject DistanceWeaponData
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
        enemyLayer = DistanceWeaponData.enemyLayer;
        image = DistanceWeaponData.image;
    }
    #endregion 

    #region Shoot Logic
    private LineRenderer lineRenderer;

    public bool OkToShoot { get; set; }
    void UpdateLaser()
    {
        lineRenderer.enabled = true;
        Vector2 mousePos = Utils.GetMouseWorldPosition();
        lineRenderer.SetPosition(0, attackPoint.position);
        lineRenderer.SetPosition(1, transform.right * 20  + transform.position);

        Vector2 direction = mousePos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);

        if (hit)
        { 
            // Walls...
           if(hit.collider.gameObject.CompareTag("Enemy"))
           {
                lineRenderer.SetPosition(1, hit.point);
                //TakeDamage
           }
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
