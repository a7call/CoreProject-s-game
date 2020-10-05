using UnityEngine;
using UnityEngine.UIElements;

public class RandomFear : MonoBehaviour
{
    public GameObject point;
    private float coordonneesX;
    private float coordonneesY;
    private Vector3 coordonnees;

    private PlayerMouvement playerMouvement;


    void Start()
    {
        playerMouvement = FindObjectOfType<PlayerMouvement>();

        coordonneesX = playerMouvement.transform.position.x;
        coordonneesY = playerMouvement.transform.position.y;
        Vector3 randomDirection = (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0.0f));
        coordonnees = new Vector3(coordonneesX, coordonneesY, 0);
        point.transform.position = randomDirection + coordonnees;
    }

}
