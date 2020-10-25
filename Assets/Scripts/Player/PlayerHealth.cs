using System.Collections;
using UnityEngine;
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
    protected override void Awake()
    {
        base.Awake();
        SetMaxHealth();
    }

    //Pour tester la fonction Take20Damage
    private void Update()
    {
        Take20Damage();
        healthBar.SetHealth(currentHealth);
    }

    public void SetMaxHealth()
    {
        currentHealth =  maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
        currentHealth -= damage;
        StartCoroutine(InvincibilityDelay());
        StartCoroutine(InvincibilityFlash());
        }
    }

    //Fonction test perd des hp
    public void Take20Damage()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentHealth -= 20;
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
}
