using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerScriptableObjectScript playerData;
    public EtatJoueur currentEtat = EtatJoueur.normal;

    public enum EtatJoueur
    {
        normal,
        fear,
    }


    public int maxHealth;
    public int maxEnergy;
    public float mooveSpeed;
    public float dashForce;
    public int dashEnergyCost;
    public int damage;
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

}
