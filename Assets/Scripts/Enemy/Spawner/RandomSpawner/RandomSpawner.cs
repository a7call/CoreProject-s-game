using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Permet de créer un objet qui invoquera des monstres aléatoirement
/// Attention, il faudra vérifier à ne pas invoquer les objets sur des élements de décor
/// </summary>

public class RandomSpawner : Enemy
{
    [SerializeField] private List<GameObject> mobs;
    [SerializeField] protected float spawnCd;
    [SerializeField] protected SpawnerScriptableObject SpawnerData;


    protected override void Awake()
    {
        healthBarGFX.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        currentState = State.Chasing;
        gameObject.GetComponent<Enemy>().isInvokedInBossRoom = true;
        SetData();
        SetMaxHealth();
        StartCoroutine(PopMobs());
    }

    protected override void Update()
    {
        Die();
    }

    protected IEnumerator PopMobs()
    {
        while (true)
        {
            int nb = (int)Random.Range(0, mobs.Count);
            GameObject mob = Instantiate(mobs[nb],transform.position,Quaternion.identity);
            mob.GetComponent<Enemy>().isInvokedInBossRoom = true;
            yield return new WaitForSeconds(spawnCd);
        }
    }

    private void SetData()
    {
        maxHealth = SpawnerData.maxHealth;
    }

    private void Die()
    {
        if (currentHealth < 1)
        {
            print("Détruire");
            Destroy(gameObject);
        }
    }

}
