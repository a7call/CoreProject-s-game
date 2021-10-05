using UnityEngine;
using System.Collections;
using System.Linq;
using Wanderer.Utils;
using System.Collections.Generic;


public class TestSpawningRoom : MonoBehaviour
{

    #region Variables 

    [Header("Waves Attributes")]
    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject secondWave;
    [SerializeField] private float[] spawnTimer;
    private List<List<GameObject>> spawnListByWave = new List<List<GameObject>>(); // Liste qui récupère tous les spawns d'ennemis
    private List<Enemy> enemiesFirstWave = new List<Enemy>();
    private List<Enemy> enemiesSecondWave = new List<Enemy>();
    private float timeBeforeActiveFight = 1f;


    [Header("Doors")]
    [SerializeField] private GameObject doors;
    private DoorManagement doorManagement;
   
    #endregion

    #region Monobehiavour

    private void Awake()
    {
        spawnListByWave.Add(Utils.FindGameObjectsInChildWithTag(firstWave, "Spawner"));
        spawnListByWave.Add(Utils.FindGameObjectsInChildWithTag(secondWave, "Spawner"));
        doorManagement = doors.GetComponent<DoorManagement>();
    }

    private void Start()
    {
        FindEnemiesInWave(enemiesFirstWave, 0);
        FindEnemiesInWave(enemiesSecondWave, 1);
    }

    private void Update()
    {
        if (areEnemiesDied(enemiesFirstWave))
        {
            StartCoroutine(ActiveFight(secondWave, 1, timeBeforeActiveFight));
        }

        if (areEnemiesDied(enemiesSecondWave)) doorManagement.OpenDoors();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) StartCoroutine(ActiveFight(firstWave, 0, timeBeforeActiveFight));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !areEnemiesDied(enemiesFirstWave)) doorManagement.CloseDoors();
    }

    #endregion

    private IEnumerator SetActiveSpawn(float _timer, GameObject _GO)
    {
        yield return new WaitForSeconds(_timer);
        _GO.SetActive(true);
    }

    private void FindEnemiesInWave(List<Enemy> _enemyWave, int _indexWave)
    {
        for (int i = 0; i < spawnListByWave[_indexWave].Count; i++)
        {
            Enemy[] enemiesArray = (from directChild in spawnListByWave[_indexWave][i].GetComponentsInChildren<Enemy>()
                                    where directChild.transform.parent == spawnListByWave[_indexWave][i].transform
                                    select directChild).ToArray();

            for (int j = 0; j < enemiesArray.Length; j++)
            {
                _enemyWave.Add(enemiesArray[j]);
            }
        }
    }

    private IEnumerator ActiveFight(GameObject _wave, int _index, float _coroutineTimer = 0)
    {
        yield return new WaitForSeconds(_coroutineTimer);
        
        _wave.SetActive(true);

        for (int i = 0; i < spawnListByWave[_index].Count; i++)
        {
            StartCoroutine(SetActiveSpawn(spawnTimer[i], spawnListByWave[_index][i]));
        }
    }

    private bool areEnemiesDied(List<Enemy> _enemyWave)
    {
        foreach (var enemy in _enemyWave)
        {
            if (enemy.enabled) return false;
        }
        return true;
    }

}
