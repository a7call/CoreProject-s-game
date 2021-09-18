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
        attackPoint = gameObject.transform.GetChild(0);
        animator = GetComponent<Animator>();
        weaponManager = GetComponentInParent<EnemyWeaponManager>();       
    }
    private void Update()
    {
       
    }
    public void StartShootingProcessCo()
    {
        StartCoroutine(weaponManager.Monster.StartShootingProcessCo());
        StartCoroutine(weaponManager.Monster.RestCo(animator));
    }

}
