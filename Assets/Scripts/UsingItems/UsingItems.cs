using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// 
/// </summary>

public class UsingItems : MonoBehaviour
{
    // Déclarations des variables
    private PlayerHealth playerHealth;
    private PlayerMouvement playerMouvement;


    public PlayerScriptableObjectScript playerData;
    public ItemScriptableObject ItemsData;

    //Je crois que c'est pas opti de faire un FindObjectOfType<> dans le Start
    private void Start()
    {
        playerMouvement = FindObjectOfType<PlayerMouvement>();

        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }

    // Ajoute des Hp
    public void AddHp()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
            
            if (playerData.maxHealth <= playerHealth.currentHealth + ItemsData.hpGiven)
            {
                playerHealth.currentHealth += ItemsData.hpGiven;
            }
            else
            {
                playerHealth.currentHealth = playerData.maxHealth;
            }
    }

    // Ajoute du Shield
    private void AddShield()
    {
        ItemsData.pointShield = playerData.maxHealth / 2;
        playerHealth.currentHealth += ItemsData.pointShield;
    }

    //// Ajoute de la MS
    //private void AddMs()
    //{
    //    //playerMouvement.
    //}

    ////
    //private void AddAs()
    //{
    //    playerAttack.
    //}

    ////Coroutine
    //private void ConsumeMSPotion()
    //{
    //    playerMouvement.speed 
    //}

    //private void ConsumeASPotion()
    //{

    //}
    
    //A voir si on les place ici ou dans l'inventaire
    //Fonction coroutine pendant laquelle les effets de la potion de MS/AS
    //Fonction qui rend impossible de reconsommer une potion pendant X temps
    //Fonction cliquer pour utiliser la potion (BUTTON)
    //Jouer une animation de prendre la potion
}
