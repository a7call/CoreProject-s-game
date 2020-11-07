using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerScriptableObjectScript playerData;
    public EtatJoueur currentEtat = EtatJoueur.normal;

    public PlayerManager playerManager;
   
    public enum EtatJoueur
    {
        normal,
        fear,
        shopping,
    }

    protected virtual void Update()
    {
        NormalMode();
    }

    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int maxEnergy;
    [HideInInspector]
    public float mooveSpeed;
    [HideInInspector]
    public float dashForce;
    [HideInInspector]
    public int dashEnergyCost;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public int energyReloadNumber;

    protected void SetData()
    {
        maxHealth = playerData.maxHealth;
        maxEnergy = playerData.maxEnergy;
        mooveSpeed = playerData.mooveSpeed;
        dashForce = playerData.dashForce;
        dashEnergyCost = playerData.dashEnergyCost;
        damage = playerData.damage;
        energyReloadNumber = playerData.energyReloadNumber;
    }

    protected virtual void Awake()
    {
        SetData();
    }

    protected virtual void NormalMode()
    {
        if(Input.GetKeyDown(KeyCode.M) == true)
        {
            currentEtat = EtatJoueur.normal;
        }
    }

}
