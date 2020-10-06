using UnityEngine;
using UnityEngine.UIElements;

public class RandomFear : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private Transform playerPos;
    private Transform enemyPos;

    private float coordonneesXPlayer;
    private float coordonneesYPlayer;
    private float coordonneesXEnemy;
    private float coordonneesYEnemy;
    
    private Vector3 coordonneesPlayer;
    private Vector3 vecEnemyPlayer;
    public Vector3 pointPos;

    private PlayerMouvement playerMouvement;
    private SpecCaC2 specCaC2;

    private void Update()
    {
        positionFearPoint();
        //transformFearPoint();
    }

    public void positionFearPoint()
    {
        playerPos = player.GetComponent<Transform>();
        coordonneesXPlayer = playerPos.position.x;
        coordonneesYPlayer = playerPos.position.y;
        coordonneesPlayer = new Vector3(coordonneesXPlayer, coordonneesYPlayer, 0);


        enemyPos = enemy.GetComponent<Transform>();
        coordonneesXEnemy = enemyPos.position.x;
        coordonneesYEnemy = enemyPos.position.y;

        vecEnemyPlayer = new Vector3(coordonneesXEnemy - coordonneesXPlayer, coordonneesYEnemy - coordonneesYPlayer, 0);

        pointPos = coordonneesPlayer - 25*vecEnemyPlayer;
    }

    //Première méthode qui est je pense moins bonne
    //void Start()
    //{
    //    playerMouvement = FindObjectOfType<PlayerMouvement>();
    //    specCaC2 = FindObjectOfType<SpecCaC2>();
    //}

    //public void transformFearPoint()
    //{
    //    coordonneesXPlayer = playerMouvement.transform.position.x;
    //    coordonneesYPlayer = playerMouvement.transform.position.y;
    //    coordonneesPlayer = new Vector3(coordonneesXPlayer, coordonneesYPlayer, 0);

    //    coordonneesXEnemy = specCaC2.transform.position.x;
    //    coordonneesYEnemy = specCaC2.transform.position.y;

    //    vecEnemyPlayer = new Vector3(coordonneesXEnemy - coordonneesXPlayer, coordonneesYEnemy - coordonneesYPlayer, 0);

    //    pointPos = coordonneesPlayer - vecEnemyPlayer;
    //}

}
