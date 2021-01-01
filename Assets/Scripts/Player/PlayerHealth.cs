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

   


    public static bool isLastChanceModule = false;

    // Lié aux modules du PowerUp
    [HideInInspector] public static bool isPowerUp = false;
    private bool isPowerUpAlreadyUse = false;

    protected override void Awake()
    {
        base.Awake();
        SetMaxHealth();
    }

    //Pour tester la fonction Take1Damage
    protected void Update()
    {
        AjustHhealth();
        UpdateUILife();
        Take1Damage();
    }

    public void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }

    private void AjustHhealth()
    {
        if (isPowerUp && !isPowerUpAlreadyUse)
        {
            maxHealth = 8;
            currentHealth += 2;
            isPowerUpAlreadyUse = true;
        }

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        // RETIRER LE HEALTH IF LORSQUE L'ON AURA FAIT LA MORT DU JOUEUR
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }
    //DontFuckModule
    public static bool IsDontFuckWithMe = false;
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
        if (IsDontFuckWithMe)
        {
            DontFuckWithMeModule.DestroyAllEnemyInRange();
            print("tes");
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

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible) {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
        }
    }

    public IEnumerator InvincibilityDelay()
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
            
            HealthContent.Find("ImagePowerUpHP").GetComponent<Image>().enabled = true;
            image4 = HealthContent.Find("ImagePowerUpHP").GetComponent<Image>();

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
