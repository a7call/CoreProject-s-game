using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] protected EnemyScriptableObject EnemyData;


    [SerializeField] protected int currentHealth;
    [SerializeField] private  int maxHealth;
    [SerializeField] private Material whiteMat;
    [SerializeField] private Material defaultMat;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    private void Start()
    {
        SetData();
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

    private void SetData()
    {
        maxHealth = EnemyData.maxHealth;
        whiteMat = EnemyData.whiteMat;
        defaultMat = EnemyData.defaultMat;
    }
     

}
