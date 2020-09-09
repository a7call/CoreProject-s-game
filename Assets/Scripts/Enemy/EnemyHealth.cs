using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected Material whiteMat;
    [SerializeField] protected Material defaultMat;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    private void Start()
    {
        SetMaxHealth();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    private void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }

    protected void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        StartCoroutine(WhiteFlash());
    }

    protected IEnumerator WhiteFlash()
    {

        spriteRenderer.material = whiteMat;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMat;

    }
     

}
