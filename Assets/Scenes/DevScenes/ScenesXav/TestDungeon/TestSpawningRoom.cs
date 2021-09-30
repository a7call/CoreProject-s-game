using UnityEngine;
using System.Linq;


public class TestSpawningRoom : MonoBehaviour
{
    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject secondWave;
    private Enemy[] enemiesFirstWave;

    private void Awake()
    {
        enemiesFirstWave = (from directChild in firstWave.GetComponentsInChildren<Enemy>()
                                           where directChild.transform.parent == firstWave.transform
                                           select directChild).ToArray();
    }

    private void Update()
    {
        if (canSecondWave()) secondWave.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            firstWave.SetActive(true);
        }
    }

    private bool canSecondWave()
    {
        foreach (var enemy in enemiesFirstWave)
        {
            if (enemy.enabled)
            {
                return false;
            }
        }
        return true;
    }
    
}
