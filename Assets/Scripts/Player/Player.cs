using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    public PlayerScriptableObjectScript playerData;
    [HideInInspector]
    public EtatJoueur currentEtat = EtatJoueur.normal;
    
    //public PlayerManager playerManager;
   
    public enum EtatJoueur
    {
        normal,
        fear,
        shopping,
        Grapping,
    }

    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int maxEnergy;
    [HideInInspector]
    public float mooveSpeed;
    //[HideInInspector]
    public float dashForce;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float energyReloadNumber;

    protected void SetData()
    {
        maxHealth = playerData.maxHealth;
        maxEnergy = playerData.maxEnergy;
        mooveSpeed = playerData.mooveSpeed;
        dashForce = playerData.dashForce;
        damage = playerData.damage;
        energyReloadNumber = playerData.energyReloadNumber;
    }

    protected virtual void Awake()
    {
        SetData();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
   
}
