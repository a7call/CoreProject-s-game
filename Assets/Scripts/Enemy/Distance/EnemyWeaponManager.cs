using System.Collections;
using UnityEngine;


public class EnemyWeaponManager : MonoBehaviour
{
    public Distance Monster { get; private set; }
    public EnemyWeapon Weapon { get; private set; }
    public SpriteRenderer WeaponSR { get; set; }

    Vector3 PositionArme = Vector3.zero;

    Vector3 PosAttackPoint = Vector3.zero;

    public Vector3 aimDirection = Vector3.zero;

    void Awake()
    {
        Weapon = GetComponentInChildren<EnemyWeapon>();
        WeaponSR = GetComponentInChildren<SpriteRenderer>();
        Monster = GetComponentInParent<UnDeadMeca>();
        
    }

    private void Start()
    {
        SetWeaponXPosition(Weapon);
        PosAttackPoint = Weapon.attackPoint.localPosition;
    }


    void Update()
    {
        MoveWeapon();
    }

    private void MoveWeapon()
    {
        aimDirection = (GetComponentInParent<Enemy>().Target.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Weapon.transform.eulerAngles = new Vector3(0, 0, angle);

        SetWeaponYPositionAndLayer(Weapon);

        if (aimDirection.x < 0f && !WeaponSR.flipY)
        {
            WeaponSR.flipY = true;
            PositionArme = new Vector3(-PositionArme.x, PositionArme.y);
            Weapon.transform.localPosition = PositionArme;
            Weapon.attackPoint.localPosition = new Vector3(PosAttackPoint.x, -PosAttackPoint.y);
            
        }
        else if (aimDirection.x > 0f && WeaponSR.flipY)
        {
            WeaponSR.flipY = false;
            PositionArme = new Vector3(-PositionArme.x, PositionArme.y);
            Weapon.transform.localPosition = PositionArme;
            Weapon.attackPoint.localPosition = new Vector3(PosAttackPoint.x, PosAttackPoint.y);
        }
        
    }
    private void SetWeaponXPosition(EnemyWeapon weapons)
    {
        PositionArme.x = weapons.offSetX;
        weapons.transform.localPosition = PositionArme;
    }

    private void SetWeaponYPositionAndLayer(EnemyWeapon _weapons)
    {
        PositionArme.y = _weapons.offSetY;
        _weapons.transform.localPosition = PositionArme;
    }
}
