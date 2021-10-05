using UnityEngine;
using System.Collections;
using System.Linq;
using Wanderer.Utils;
using System.Collections.Generic;
using UnityEditor;


[System.Serializable]
public class SpawnTimerArray
{
    [field: SerializeField]
    public float[] Spawns { get; set; }

}

public enum enumWaves { Wave1, Wave2};


public class TestSpawningRoom : MonoBehaviour
{
    #region Variables 
    
    [Header("Waves")]
    // Waves
    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject secondWave;
    private List<Enemy> enemiesFirstWave = new List<Enemy>();
    private List<Enemy> enemiesSecondWave = new List<Enemy>();
    
    // Spawners
    [NamedArrayAttribute(typeof(enumWaves))]
    [SerializeField] private SpawnTimerArray[] spawnTimers = new SpawnTimerArray[2];
    private List<List<GameObject>> spawnListByWave = new List<List<GameObject>>(); // Liste qui récupère tous les spawns d'ennemis
    private float timeBeforeActiveFight = 1f;


    [Header("Doors")]
    [SerializeField] private GameObject doors;
    private DoorManagement doorManagement;
    private BoxCollider2D roomBoxCollider2D;
   
    #endregion

    #region Monobehiavour

    private void Awake()
    {
        spawnListByWave.Add(Utils.FindGameObjectsInChildWithTag(firstWave, "Spawner"));
        spawnListByWave.Add(Utils.FindGameObjectsInChildWithTag(secondWave, "Spawner"));
        doorManagement = doors.GetComponent<DoorManagement>();
        roomBoxCollider2D = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        FindEnemiesInWave(enemiesFirstWave, 0);
        FindEnemiesInWave(enemiesSecondWave, 1);
        SetEnemiesFleeCollider(enemiesFirstWave);
        SetEnemiesFleeCollider(enemiesSecondWave);
    }

    private void Update()
    {
        if (areEnemiesDied(enemiesFirstWave)) StartCoroutine(ActiveFight(secondWave, 1, timeBeforeActiveFight));   
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
            StartCoroutine(SetActiveSpawn(spawnTimers[_index].Spawns[i], spawnListByWave[_index][i]));
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

    private void SetEnemiesFleeCollider(List<Enemy> _enemyWave)
    {
        foreach (Enemy enemy in _enemyWave)
        {
            enemy.RoomFloorCollider = roomBoxCollider2D;
        }
    }

}
