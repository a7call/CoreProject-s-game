using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// 
/// </summary>

public class UsingItems : MonoBehaviour
{
    // Déclarations des variables
    private Player player;



    public PlayerScriptableObjectScript playerData;
    public ItemScriptableObject ItemsData;

    //Je crois que c'est pas opti de faire un FindObjectOfType<> dans le Start
    private void Start()
    {
        player = FindObjectOfType<Player>();

        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Ajoute des Hp
    public void AddHp()
    {
        player = FindObjectOfType<Player>();
            
            if (playerData.maxHealth <= player.currentHealth + ItemsData.hpGiven)
            {
                player.currentHealth += ItemsData.hpGiven;
            }
            else
            {
                player.currentHealth = playerData.maxHealth;
            }
    }

    // Ajoute du Shield
    private void AddShield()
    {
        ItemsData.pointShield = playerData.maxHealth / 2;
        player.currentHealth += ItemsData.pointShield;
    }

    //// Ajoute de la MS
    //private void AddMs()
    //{
    //    //player.
    //}

    ////
    //private void AddAs()
    //{
    //    playerAttack.
    //}

    ////Coroutine
    //private void ConsumeMSPotion()
    //{
    //    player.speed 
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
