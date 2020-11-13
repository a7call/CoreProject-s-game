using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Classe player health gérant la vie du joueur 
/// </summary>

public class PlayerHealth : Player
{

    public int currentHealth;
    private bool isInvincible;
    public float InvincibilityFlashDelay;
    public float InvincibleDelay;
    public HealthBar healthBar;
    

    public SpriteRenderer graphics;

    public Image image1;
    public Image image2;
    public Image image3;

    public Sprite emptyHearth;
    public Sprite halfHearth;
    public Sprite fullHeart;

    //LastChanceModule
    [HideInInspector]
    public static bool isLastChanceModule = false;

    protected override void Awake()
    {
        base.Awake();
        SetMaxHealth();
    }

    //Pour tester la fonction Take1Damage
    protected override void Update()
    {
        AddLifePlayer();
        UpdateUILife();
        Take1Damage();
       // healthBar.SetHealth(currentHealth);
    }

    public void SetMaxHealth()
    {
        currentHealth =  maxHealth;
       // healthBar.SetMaxHealth(maxHealth);
        
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
        currentHealth -= damage;
        StartCoroutine(InvincibilityDelay());
        StartCoroutine(InvincibilityFlash());
        }

        if (currentHealth <= 0 && !isLastChanceModule)
        {
            //A mettre ici par la suite
            //image1.sprite = emptyHearth;
            //Debug.Log("Mort");
        }

        else if (currentHealth <=0 && isLastChanceModule)
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
            currentHealth -= 1;
        }
    }

    private IEnumerator InvincibilityFlash()
    {
        while(isInvincible){
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

    private void AddLifePlayer()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentHealth += 1;
        }
    }

    private void UpdateUILife()
    {
        if(currentHealth==maxHealth)
        {
            image1.sprite = fullHeart;
            image2.sprite = fullHeart;
            image3.sprite = fullHeart;
        }
        else if (currentHealth == maxHealth - 1)
        {
            image1.sprite = fullHeart;
            image2.sprite = fullHeart;
            image3.sprite = halfHearth;
        }
        else if (currentHealth == maxHealth - 2)
        {
            image1.sprite = fullHeart;
            image2.sprite = fullHeart;
            image3.sprite = emptyHearth;
        }
        else if (currentHealth == maxHealth - 3)
        {
            image1.sprite = fullHeart;
            image2.sprite = halfHearth;
            image3.sprite = emptyHearth;
        }
        else if (currentHealth == maxHealth - 4)
        {
            image1.sprite = fullHeart;
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
}
