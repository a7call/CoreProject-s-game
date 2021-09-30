using UnityEngine;
using System.Collections;
using System.Linq;
using Wanderer.Utils;
using System.Collections.Generic;


public class TestSpawningRoom : MonoBehaviour
{

    #region Variables 

    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject secondWave;
    [SerializeField] private float[] spawnTimer;

    private List<List<GameObject>> spawnListByWave = new List<List<GameObject>>(); // Liste qui récupère tous les spawns d'ennemis
    private List<Enemy> enemiesFirstWave = new List<Enemy>();
   
    #endregion

    #region Monobehiavour

    private void Awake()
    {
        spawnListByWave.Add(Utils.FindGameObjectsInChildWithTag(firstWave, "Spawner"));
        spawnListByWave.Add(Utils.FindGameObjectsInChildWithTag(secondWave, "Spawner"));
    }

    private void Start()
    {
        FindEnemiesInWave(0);
    }

    private void Update()
    {
        if (canLaunchSecondWave())
        {
            secondWave.SetActive(true);

            for (int i = 0; i < spawnListByWave[1].Count; i++)
            {
                StartCoroutine(SetActiveSpawn(spawnTimer[i], spawnListByWave[1][i]));
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            firstWave.SetActive(true);

            for (int i = 0; i < spawnListByWave[0].Count; i++)
            {
                StartCoroutine(SetActiveSpawn(spawnTimer[i], spawnListByWave[0][i]));
            }
        }
    }

    #endregion

    private IEnumerator SetActiveSpawn(float _timer, GameObject _GO)
    {
        yield return new WaitForSeconds(_timer);
        _GO.SetActive(true);
    }

    private void FindEnemiesInWave(int _indexWave)
    {
        for (int i = 0; i < spawnListByWave[_indexWave].Count; i++)
        {
            Enemy[] enemiesArray = (from directChild in spawnListByWave[_indexWave][i].GetComponentsInChildren<Enemy>()
                                    where directChild.transform.parent == spawnListByWave[_indexWave][i].transform
                                    select directChild).ToArray();

            for (int j = 0; j < enemiesArray.Length; j++)
            {
                enemiesFirstWave.Add(enemiesArray[j]);
            }
        }
    }

    private bool canLaunchSecondWave()
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
