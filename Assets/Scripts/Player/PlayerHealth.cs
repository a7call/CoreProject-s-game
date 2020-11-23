using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Classe player health gérant la vie du joueur 
/// </summary>

public class PlayerHealth : Player
{
    //[HideInInspector]
    public int currentHealth;
    public int currentArmor;
    private int maxArmor = 2;
    private bool isInvincible;
    public float InvincibilityFlashDelay;
    public float InvincibleDelay;


    public SpriteRenderer graphics;

    // Pour le UI de la vie
    //[HideInInspector]
    public Image image1;
    //[HideInInspector]
    public Image image2;
    //[HideInInspector]
    public Image image3;
    //[HideInInspector]
    public Image image4;
    //[HideInInspector]
    public Sprite emptyHearth;
    //[HideInInspector]
    public Sprite halfHearth;
    //[HideInInspector]
    public Sprite fullHearth;

    // Pour le UI de l'armor
    //[HideInInspector]
    public Image imageArmor;
    //[HideInInspector]
    public Sprite halfArmor;
    //[HideInInspector]
    public Sprite fullArmor;


    public static bool isLastChanceModule = false;

    // Lié aux modules du PowerUp
    [HideInInspector] public static bool isPowerUp = false;

    protected override void Awake()
    {
        base.Awake();
        SetMaxHealth();
    }

    //Pour tester la fonction Take1Damage
    protected void Update()
    {
        UpdateUILife();
        Take1Damage();
    }

    public void SetMaxHealth()
    {
        currentHealth = maxHealth;

    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            if (currentArmor > 0)
            {
                currentArmor -= damage;
                StartCoroutine(InvincibilityDelay());
                StartCoroutine(InvincibilityFlash());
            }
            else
            {
                currentHealth -= damage;
                StartCoroutine(InvincibilityDelay());
                StartCoroutine(InvincibilityFlash());
            }
        }

        if (currentHealth <= 0 && !isLastChanceModule)
        {
            //A mettre ici par la suite;
            //image1.sprite = emptyHearth;
            //Debug.Log("Mort");
        }

        else if (currentHealth <= 0 && isLastChanceModule)
        {
            currentHealth = maxHealth;
            isLastChanceModule = false;
        }
    }

    //Fonction test perd des hp
    public void Take1Damage()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (currentArmor > 0)
            {
                currentArmor -= damage;
            }
            else
            {
                currentHealth -= damage;
            }
        }
    }

    private IEnumerator InvincibilityFlash()
    {
        while (isInvincible) {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
        }
    }

    private IEnumerator InvincibilityDelay()
    {
        isInvincible = true;
        yield return new WaitForSeconds(InvincibleDelay);
        isInvincible = false;
    }

    public void AddLifePlayer(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    public void AddArmorPlayer(int _armor)
    {
        currentArmor += _armor;
        if (currentArmor > maxArmor)
        {
            currentArmor = maxArmor;
        }
    }

    // Masquée car très longue et redondante
    private void UpdateUILife()
    {
        if (currentArmor == maxArmor)
        {
            imageArmor.enabled = true;
            imageArmor.sprite = fullArmor;
        }
        else if(currentArmor == maxArmor -1)
        {
            imageArmor.enabled = true;
            imageArmor.sprite = halfArmor;
        }
        else
        {
            imageArmor.enabled = false;
        }


        if (isPowerUp == false)
        {
            if (currentHealth == maxHealth)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = fullHearth;
            }
            else if (currentHealth == maxHealth - 1)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = halfHearth;
            }
            else if (currentHealth == maxHealth - 2)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 3)
            {
                image1.sprite = fullHearth;
                image2.sprite = halfHearth;
                image3.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 4)
            {
                image1.sprite = fullHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 5)
            {
                image1.sprite = halfHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 6)
            {
                image1.sprite = emptyHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
            }
        }
        else
        {
            image4.enabled = true;
            if (currentHealth == maxHealth)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = fullHearth;
                image4.sprite = fullHearth;
            }
            else if (currentHealth == maxHealth - 1)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = fullHearth;
                image4.sprite = halfHearth;
            }
            else if (currentHealth == maxHealth - 2)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = fullHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 3)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = halfHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 4)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 5)
            {
                image1.sprite = fullHearth;
                image2.sprite = halfHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 6)
            {
                image1.sprite = fullHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 7)
            {
                image1.sprite = halfHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 8)
            {
                image1.sprite = emptyHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
        }
    }

}
