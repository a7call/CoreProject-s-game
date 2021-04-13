
using UnityEngine;
using Wanderer.Utils;

public class LaserGun : Weapons, IShootableWeapon
{
    public LineRenderer lineRenderer;

    public bool OkToShoot { get; set;}

    public WeaponScriptableObject DistanceWeaponData
    {
        get
        {
            return LaserDatas;
        }
    }
    public CollingWeaponScriptableObject LaserDatas;
    protected override void Awake()
    {
        SetData();
        base.Awake();
    }

    void Start()
    {
        DisableLaser();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            EnableLaser();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateLaser();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();
        }

        
    }

    protected virtual void SetData()
    {
        enemyLayer = DistanceWeaponData.enemyLayer;
        image = DistanceWeaponData.image;
    }


    void EnableLaser()
    {
        lineRenderer.enabled = true;
    }

    void UpdateLaser()
    {
        Vector2 mousePos = Utils.GetMouseWorldPosition();
        lineRenderer.SetPosition(0, attackPoint.position);
        lineRenderer.SetPosition(1, transform.right*50);
        print(transform.right);
        print(mousePos);

        Vector2 direction = mousePos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);
        //if (hit)
        //    lineRenderer.SetPosition(1, hit.point);

    }

    void DisableLaser()
    {
        lineRenderer.enabled = false;
    }

    public void StartShootingProcess()
    {
        return;
    }

}
