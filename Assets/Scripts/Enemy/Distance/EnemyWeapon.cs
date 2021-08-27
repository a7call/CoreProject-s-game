using System.Collections;
using UnityEngine;


public class EnemyWeapon : MonoBehaviour
{
    EnemyWeaponManager weaponManager;
    public Transform attackPoint { get;  set; }

    public float offSetX = 0;

    public float offSetY = 0;

    public Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        weaponManager = GetComponentInParent<EnemyWeaponManager>();
        attackPoint = gameObject.transform.GetChild(0);
    }
    private void Update()
    {
       
    }
    public void CanShootCO()
    {
        StartCoroutine(weaponManager.Monster.CanShootCO());
    }

}
