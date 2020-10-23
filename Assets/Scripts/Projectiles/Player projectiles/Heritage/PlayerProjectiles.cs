using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerProjectiles : MonoBehaviour
{
    protected GameObject player;
    protected GameObject weapon;
    protected Vector3 dir;
    protected Transform playerTransform;
    protected Transform weaponAttackP;
    protected float speed;
    protected PlayerProjectileScriptableObject PlayerProjectileData;


    protected virtual void Awake()
    {
        SetData();
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        weaponAttackP = weapon.GetComponent<Weapons>().attackPoint;
        playerTransform = player.GetComponent<Transform>();
        dir = (weaponAttackP.position - playerTransform.position).normalized;

    }


    protected virtual void Update()
    {

    }

    protected virtual void Launch()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
    void SetData()
    {
        speed = PlayerProjectileData.speed;
    }
}
