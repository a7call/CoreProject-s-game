using UnityEngine;
using System.Collections;

/// <summary>
/// Classe de l'inventaire
/// 
/// </summary>

public class UsingItems : ItemObjects
{
    // Déclarations d'autres variables
    private PlayerHealth playerHealth;
    private PlayerMouvement playerMouvement;
    private PlayerAttack playerAttack;

    // Déclarations des items
    [SerializeField] protected ItemObjects ItemObjectsData;

    //Méthode SetData()
    protected virtual void SetData()
    {
        id = ItemObjectsData.id;
        nameObject = ItemObjectsData.nameObject;
        description = ItemObjectsData.description;
        Image = ItemObjectsData.Image;
        coastAtTrader = ItemObjectsData.coastAtTrader;
        hpGiven = ItemObjectsData.hpGiven;
        speed = ItemObjectsData.speed;
        speedDuration = ItemObjectsData.speedDuration;
        increasedDamages = ItemObjectsData.increasedDamages;
        attackSpeed = ItemObjectsData.attackSpeed;
        pointShield = ItemObjectsData.pointShield;
    }

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    private void ConsumeHpPotion()
    {
        playerHealth.currentHealth += hpGiven;
    }

    ////Coroutine
    //private void ConsumeMSPotion()
    //{
    //    playerMouvement.speed 
    //}

    //private void ConsumeASPotion()
    //{

    //}
}
